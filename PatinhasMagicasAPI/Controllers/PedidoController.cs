using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Services;

namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [Authorize]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _pedidoService;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController(PedidoService pedidoService, IPedidoRepository pedidoRepository)
        {
            _pedidoService = pedidoService;
            _pedidoRepository = pedidoRepository;
        }

        // --- GET TODOS OS PEDIDOS ---
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoOutputDTO>>> GetAll()
        {
            //var pedidos = await _pedidoRepository.GetAllAsync();
            //if (!pedidos.Any())
            //    return NotFound(new { message = "Nenhum pedido encontrado." });

            //var pedidosDTO = _pedidoService.Map(pedidos); // Se quiser manter método Map no service ou usar AutoMapper direto
            return Ok();
        }

        // --- GET PEDIDOS POR USUÁRIO ---
        [HttpGet("Usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<PedidoOutputDTO>>> GetPedidosByUsuarioId(int usuarioId)
        {
            var pedidos = await _pedidoService.GetPedidosByUsuarioId(usuarioId);
            if (pedidos == null || !pedidos.Any())
                return NotFound(new { message = "Nenhum pedido encontrado para este usuário." });

            return Ok(pedidos);
        }

        // --- GET PEDIDOS PAGINADOS / DASHBOARD ---
        [HttpGet("paged")]
        public async Task<ActionResult<DashboardPedidoDTO>> GetPedidosPaginados([FromQuery] PedidoFiltroDTO filtro)
        {
            var dashboardPedido = await _pedidoService.GetPedidosPaginados(filtro);

            if (!dashboardPedido.PedidoOutputDTO.Any())
                return Ok(new
                {
                    success = true,
                    message = "Nenhum pedido encontrado!",
                    pedidos = new List<PedidoOutputDTO>()
                });

            return Ok(dashboardPedido);
        }

        // --- GET PEDIDO POR ID ---
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoOutputDTO>> GetById(int id)
        {
            var pedido = await _pedidoService.GetByIdAsync(id);
            if (pedido == null)
                return NotFound(new { message = "Pedido não encontrado." });

            return Ok(pedido);
        }

        // --- POST CRIAR PEDIDO ---
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PedidoInputDTO pedidoInputDTO)
        {
            var pedidoOutputDTO = await _pedidoService.CreatePedidoAsync(pedidoInputDTO);
            return Ok(new
            {
                success = true,
                message = "Pedido cadastrado com sucesso!",
                pedidoId = pedidoOutputDTO.Id
            });
        }

        // --- PUT ATUALIZAR PEDIDO ---
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PedidoInputDTO pedidoInputDTO)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido == null)
                return NotFound(new { message = "Pedido não encontrado." });

            // Atualiza propriedades
            pedido.DataPedido = pedidoInputDTO.DataPedido;
            pedido.StatusPedidoId = pedidoInputDTO.StatusPedidoId;
            pedido.UsuarioId = pedidoInputDTO.UsuarioId;

            await _pedidoRepository.UpdateAsync(pedido);
            return NoContent();
        }

        // --- DELETE PEDIDO ---
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido == null)
                return NotFound(new { message = "Pedido não encontrado." });

            await _pedidoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
