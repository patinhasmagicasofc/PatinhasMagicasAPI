using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;


namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPedidoController : ControllerBase
    {
        private readonly IItemPedidoRepository _itemPedidoRepository;

        public ItemPedidoController(IItemPedidoRepository itemPedidoRepository)
        {
            _itemPedidoRepository = itemPedidoRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var itensPedido = await _itemPedidoRepository.GetAllAsync();

            if (!itensPedido.Any())
                return NotFound();

            var itensPedidoInputDTO = itensPedido.Select(i => new ItemPedidoInputDTO
            {
                PedidoId = i.PedidoId,
                PrecoUnitario = i.PrecoUnitario,
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade,
            }).ToList();

            return Ok(itensPedidoInputDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var itemPedido = await _itemPedidoRepository.GetByIdAsync(id);

            if (itemPedido == null)
                return NotFound();

            var itemPedidoDTO = new ItemPedidoInputDTO
            {
                PedidoId = itemPedido.PedidoId,
                PrecoUnitario = itemPedido.PrecoUnitario,
                ProdutoId = itemPedido.ProdutoId,
                Quantidade = itemPedido.Quantidade,
            };

            return Ok(itemPedidoDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ItemPedidoInputDTO pedidoInputDTO)
        {
            var pedido = new ItemPedido
            {
                PedidoId = pedidoInputDTO.PedidoId,
                PrecoUnitario = pedidoInputDTO.PrecoUnitario,
                ProdutoId = pedidoInputDTO.ProdutoId,
                Quantidade = pedidoInputDTO.Quantidade
            };

            await _itemPedidoRepository.AddAsync(pedido);

            var itemPedidoOutputDTO = new ItemPedidoOutputDTO
            {
                Id = pedido.Id,
                PedidoId = pedido.PedidoId,
                ProdutoId = pedido.ProdutoId,
                Quantidade = pedido.Quantidade,
                PrecoUnitario = pedido.PrecoUnitario
            };

            return CreatedAtAction(nameof(GetById), new { id = itemPedidoOutputDTO.Id }, itemPedidoOutputDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ItemPedidoInputDTO pedidoInputDTO)
        {
            var itemPedido = await _itemPedidoRepository.GetByIdAsync(id);

            if (itemPedido == null)
                return NotFound();

            itemPedido = new ItemPedido
            {
                Id = id,
                PedidoId = pedidoInputDTO.PedidoId,
                PrecoUnitario = pedidoInputDTO.PrecoUnitario,
                ProdutoId = pedidoInputDTO.ProdutoId,
                Quantidade = pedidoInputDTO.Quantidade
            };

            await _itemPedidoRepository.UpdateAsync(itemPedido);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var itemPedido = await _itemPedidoRepository.GetByIdAsync(id);
            if (itemPedido == null)
                return NotFound();

            await _itemPedidoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
