using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
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

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<AgendamentoDetalhesDTO>>> GetAgendamentosByUsuario(int usuarioId)
        {
            var agendamentos = await _agendamentoService.GetAgendamentosByUsuarioAsync(usuarioId);
            return Ok(agendamentos);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgendamentoDetalhesDTO>>> GetAll()
        {
            var agendamentos = await _agendamentoService.GetAllAsync();

            //if (!pedidos.Any())
            //    return NotFound();

            //return Ok(pedidosDTO);
            return Ok(agendamentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var agendamento = await _agendamentoService.GetByIdAsync(id);
            if (agendamento == null)
                return NotFound(new { message = "Agendamento não encontrado" });

            return Ok(agendamento);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AgendamentoCreateDTO agendamentoInputDTO)
        {
            var agendamento = await _agendamentoService.CriarAgendamentoAsync(agendamentoInputDTO);

            if (agendamento == null) return Ok(new { success = false, message = "Erro ao tentar agendar!" });

            return Ok(new { success = true, message = "Agendamento realizado com sucesso!", agendamento = agendamento });

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