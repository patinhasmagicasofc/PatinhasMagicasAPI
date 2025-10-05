using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services;


namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly PedidoService _pedidoService;

        public PedidoController(IPedidoRepository pedidoRepository, PedidoService pedidoService)
        {
            _pedidoRepository = pedidoRepository;
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoOutputDTO>>> GetAll()
        {
            var pedidos = await _pedidoRepository.GetAllAsync();

            if (!pedidos.Any())
                return NotFound();

            var pedidosDTO = pedidos.Select(p => new PedidoOutputDTO
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                ClienteId = p.ClienteId,
                DataPedido = p.DataPedido,
                StatusPedidoId = p.StatusPedidoId,
                StatusPedido = p.StatusPedido.Nome,
                NomeCliente = p.Cliente?.Nome,
                FormaPagamento = _pedidoService.GetFormaPagamento(p),
                ValorTotal = _pedidoService.GetValorTotalPedido(p),
                StatusPagamento = p.Pagamentos.Select(p => p.StatusPagamento.Nome).FirstOrDefault()
            }).ToList();

            return Ok(pedidosDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);

            if (pedido == null)
                return NotFound();

            var pedidoDTO = new PedidoOutputDTO
            {
                Id = pedido.Id,
                DataPedido = pedido.DataPedido,
                ClienteId = pedido.ClienteId,
                StatusPedidoId = pedido.StatusPedidoId,
                UsuarioId = pedido.UsuarioId,
                StatusPedido = pedido.StatusPedido.Nome,
                StatusPagamento = pedido.Pagamentos.Select(p => p.StatusPagamento.Nome).FirstOrDefault(),

                UsuarioOutputDTO = new UsuarioOutputDTO
                {
                    Nome = pedido.Cliente.Nome,
                    Email = pedido.Cliente.Email,
                    Telefone = pedido.Cliente.Telefone,
                    EnderecoOutputDTO = new EnderecoOutputDTO
                    {
                        Logradouro = pedido.Cliente.Endereco.Logradouro,
                        Numero = pedido.Cliente.Endereco.Numero,
                        Cidade = pedido.Cliente.Endereco.Cidade,
                        Estado = pedido.Cliente.Endereco.Estado,
                        CEP = pedido.Cliente.Endereco.CEP
                    }

                },
                ItemPedidoOutputDTOs = pedido.ItensPedido.Select(ip => new ItemPedidoOutputDTO
                {
                    Id = ip.Id,
                    PedidoId = ip.PedidoId,
                    ProdutoId = ip.ProdutoId,
                    Quantidade = ip.Quantidade,
                    PrecoUnitario = ip.PrecoUnitario,
                    ProdutoOutputDTO = new ProdutoOutputDTO
                    {
                        Nome = ip.Produto.Nome,
                        Codigo = ip.Produto.Codigo,
                        Preco = ip.Produto.Preco,
                        Foto = ip.Produto.Foto
                    }

                }).ToList(),
            };

            return Ok(pedidoDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PedidoInputDTO pedidoInputDTO)
        {
            var pedido = new Pedido
            {
                DataPedido = pedidoInputDTO.DataPedido,
                ClienteId = pedidoInputDTO.ClienteId,
                StatusPedidoId = pedidoInputDTO.StatusPedidoId,
                UsuarioId = pedidoInputDTO.UsuarioId
            };

            await _pedidoService.CreatePedidoAsync(pedido);

            var pedidoOutputDTO = new PedidoOutputDTO
            {
                Id = pedido.Id,
                DataPedido = pedido.DataPedido,
                ClienteId = pedido.ClienteId,
                StatusPedidoId = pedido.StatusPedidoId,
                UsuarioId = pedido.UsuarioId
            };

            return CreatedAtAction(nameof(GetById), new { id = pedidoOutputDTO.Id }, pedidoOutputDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PedidoInputDTO pedidoInputDTO)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);

            if (pedido == null)
                return NotFound();

            pedido = new Pedido
            {
                Id = id,
                DataPedido = pedidoInputDTO.DataPedido,
                ClienteId = pedidoInputDTO.ClienteId,
                StatusPedidoId = pedidoInputDTO.StatusPedidoId,
                UsuarioId = pedidoInputDTO.UsuarioId
            };

            await _pedidoRepository.UpdateAsync(pedido);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido == null)
                return NotFound();

            await _pedidoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
