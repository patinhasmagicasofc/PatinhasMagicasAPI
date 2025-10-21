using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [Authorize]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentoController(IAgendamentoService agendamentoService)
        {
            _agendamentoService = agendamentoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoOutputDTO>>> GetAll()
        {
            //var pedidos = await _pedidoRepository.GetAllAsync();

            //if (!pedidos.Any())
            //    return NotFound();

            //return Ok(pedidosDTO);
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            //var pedido = await _agendamentoService.GetByIdAsync(id);

            // if (pedido == null)
            // return NotFound();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AgendamentoCreateDTO agendamentoInputDTO)
        {
            var agendamento = await _agendamentoService.CriarAgendamentoAsync(agendamentoInputDTO);

            return Ok(agendamento);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AgendamentoInputDTO agendamentoInputDTO)
        {
            //var pedido = await _agendamentoService.GetByIdAsync(id);

            //if (pedido == null)
            //    return NotFound();

            //pedido = new Pedido
            //{
            //    Id = id,
            //    DataPedido = pedidoInputDTO.DataPedido,
            //    StatusPedidoId = pedidoInputDTO.StatusPedidoId,
            //    UsuarioId = pedidoInputDTO.UsuarioId
            //};

            //await _pedidoRepository.UpdateAsync(pedido);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //var pedido = await _agendamentoService.GetByIdAsync(id);
            //if (pedido == null)
            //    return NotFound();

            //await _agendamentoService.DeleteAsync(id);
            return NoContent();
        }
    }
}