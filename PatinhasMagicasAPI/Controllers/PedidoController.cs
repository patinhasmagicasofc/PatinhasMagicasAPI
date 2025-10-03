using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;


namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
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
                NomeCliente = p.Cliente?.Nome,
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
 
                UsuarioOutputDTO = new UsuarioOutputDTO
                {
                    Nome = pedido.Cliente.Nome,
                    Email = pedido.Cliente.Email,
                    Telefone = pedido.Cliente.Telefone

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
                        Preco = ip.Produto.Preco
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

            await _pedidoRepository.AddAsync(pedido);

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
