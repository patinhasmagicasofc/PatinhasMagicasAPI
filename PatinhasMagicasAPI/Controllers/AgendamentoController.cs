using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoService _service;

        public AgendamentoController(IAgendamentoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] AgendamentoInputDTO dto)
        {
            await _service.CriarAsync(dto);
            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _service.ListarAsync();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var agendamento = await _service.BuscarPorIdAsync(id);
            if (agendamento == null) return NotFound();
            return Ok(agendamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _service.DeletarAsync(id);
            return NoContent();
        }
    }
}