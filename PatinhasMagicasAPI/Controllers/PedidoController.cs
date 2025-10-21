using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
                DataPedido = p.DataPedido,
                StatusPedidoId = p.StatusPedidoId,
                StatusPedido = p.StatusPedido.Nome,
                NomeCliente = p.Usuario?.Nome,
                ValorPedido = _pedidoService.GetValorPedido(p),
                FormaPagamento = _pedidoService.GetFormaPagamento(p),
                StatusPagamento = p.Pagamentos.Select(p => p.StatusPagamento.Nome).FirstOrDefault()
            }).ToList();

            return Ok(pedidosDTO);
        }


        [HttpGet("paged")]
        public async Task<ActionResult<IEnumerable<PedidoOutputDTO>>> GetAll([FromQuery] PedidoFiltroDTO filtro)
        {
            var dashboardPedido = await _pedidoService.GetPedidosPaginados(filtro);
            if (!dashboardPedido.PedidoOutputDTO.Any())
                return Ok(new { success = true, message = "Nenhum pedido encontrado!", pedidos = new List<PedidoOutputDTO>() });

            return Ok(dashboardPedido);
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
                StatusPedidoId = pedido.StatusPedidoId,
                UsuarioId = pedido.UsuarioId,
                StatusPedido = pedido.StatusPedido.Nome,
                StatusPagamento = pedido.Pagamentos.Select(p => p.StatusPagamento.Nome).FirstOrDefault(),

                UsuarioOutputDTO = new UsuarioOutputDTO
                {
                    Nome = pedido.Usuario.Nome,
                    Email = pedido.Usuario.Email,
                    Telefone = pedido.Usuario.Telefone,
                    Endereco = new EnderecoOutputDTO
                    {
                        Logradouro = pedido.Usuario.Endereco.Logradouro,
                        Numero = pedido.Usuario.Endereco.Numero,
                        Cidade = pedido.Usuario.Endereco.Cidade,
                        Estado = pedido.Usuario.Endereco.Estado,
                        CEP = pedido.Usuario.Endereco.CEP
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
                        UrlImagem = ip.Produto.UrlImagem
                    }

                }).ToList(),
            };

            return Ok(pedidoDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PedidoInputDTO pedidoInputDTO)
        {

            var pedidoOutputDTO = await _pedidoService.CreatePedidoAsync(pedidoInputDTO);

            return Ok(new { success = true, message = "Pedido cadastrado com sucesso !", pedido = pedidoOutputDTO.Id });
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
