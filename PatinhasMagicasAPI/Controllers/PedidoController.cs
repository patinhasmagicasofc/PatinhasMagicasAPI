using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                NomeUsuario = p.Usuario?.Nome,
                ValorPedido = _pedidoService.GetValorPedido(p),
                FormaPagamento = _pedidoService.GetFormaPagamento(p),
                StatusPagamento = p.Pagamentos.Select(p => p.StatusPagamento.Nome).FirstOrDefault()
            }).ToList();

            return Ok(pedidosDTO);
        }

        [HttpGet("Usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<PedidoOutputDTO>>> GetPedidosByUsuarioId(int usuarioId)
        {
            var pedidos = await _pedidoService.GetPedidosByUsuarioId(usuarioId);

            if (pedidos == null || !pedidos.Any())
                return NotFound(new { mensagem = "Nenhum pedido encontrado para este usuário." });

            return Ok(pedidos);
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
            var pedido = await _pedidoService.GetByIdAsync(id);

            if (pedido == null)
                return NotFound();

           
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PedidoInputDTO pedidoInputDTO)
        {

            var pedidoOutputDTO = await _pedidoService.CreatePedidoAsync(pedidoInputDTO);

            return Ok(new { success = true, message = "Pedido cadastrado com sucesso !", pedidoId = pedidoOutputDTO.Id });
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
