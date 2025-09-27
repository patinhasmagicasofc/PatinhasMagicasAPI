using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoRepository _agendamentoRepository;

        public AgendamentoController(IAgendamentoRepository agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var agendamentos = await _agendamentoRepository.GetAllAsync();

            if (!agendamentos.Any())
                return NotFound();

            var agendamentoDTOs = agendamentos.Select(a => new AgendamentoOutputDTO
            {
                Id = a.Id,
                DataAgendamento = a.DataAgendamento,
                DataCadastro = a.DataCadastro,
                PedidoId = a.PedidoId,
                IdStatusAgendamento = a.IdStatusAgendamento
            }).ToList();

            return Ok(agendamentoDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var agendamento = await _agendamentoRepository.GetByIdAsync(id);

            if (agendamento == null)
                return NotFound();

            var agendamentoDTO = new AgendamentoOutputDTO
            {
                Id = agendamento.Id,
                DataAgendamento = agendamento.DataAgendamento,
                DataCadastro = agendamento.DataCadastro,
                PedidoId = agendamento.PedidoId,
                IdStatusAgendamento = agendamento.IdStatusAgendamento
            };

            return Ok(agendamentoDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AgendamentoInputDTO dto)
        {
            var agendamento = new Agendamento
            {
                DataAgendamento = dto.DataAgendamento,
                DataCadastro = dto.DataCadastro,
                PedidoId = dto.PedidoId,
                IdStatusAgendamento = dto.IdStatusAgendamento
            };

            await _agendamentoRepository.AddAsync(agendamento);

            var agendamentoOutput = new AgendamentoOutputDTO
            {
                Id = agendamento.Id,
                DataAgendamento = agendamento.DataAgendamento,
                DataCadastro = agendamento.DataCadastro,
                PedidoId = agendamento.PedidoId,
                IdStatusAgendamento = agendamento.IdStatusAgendamento
            };

            return CreatedAtAction(nameof(GetById), new { id = agendamento.Id }, agendamentoOutput);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AgendamentoInputDTO dto)
        {
            var agendamento = await _agendamentoRepository.GetByIdAsync(id);

            if (agendamento == null)
                return NotFound();

            agendamento.DataAgendamento = dto.DataAgendamento;
            agendamento.DataCadastro = dto.DataCadastro;
            agendamento.PedidoId = dto.PedidoId;
            agendamento.IdStatusAgendamento = dto.IdStatusAgendamento;

            await _agendamentoRepository.UpdateAsync(agendamento);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var agendamento = await _agendamentoRepository.GetByIdAsync(id);
            if (agendamento == null)
                return NotFound();

            await _agendamentoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
