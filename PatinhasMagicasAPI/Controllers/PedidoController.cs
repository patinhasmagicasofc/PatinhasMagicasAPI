using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services;
using PatinhasMagicasAPI.Services.Interfaces;


namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [Authorize]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoRepository pedidoRepository, IPedidoService pedidoService)
        {
            _pedidoRepository = pedidoRepository;
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoOutputDTO>>> GetAll()
        {
            var pedidoOutputDTOs = await _pedidoService.GetAllAsync();

            if (!pedidoOutputDTOs.Any())
                return NotFound();

            return Ok(pedidoOutputDTOs);
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
        public async Task<ActionResult> Post([FromBody] PedidoCompletoInputDTO pedidoInputDTO)
        {
            var pedidoOutputDTO = await _pedidoService.CreatePedidoAsync(pedidoInputDTO);

            return Ok(new { success = true, message = "Pedido cadastrado com sucesso !", pedidoId = pedidoOutputDTO.Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PedidoUpdateDTO pedidoUpdateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _pedidoService.Update(id, pedidoUpdateDTO);

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
