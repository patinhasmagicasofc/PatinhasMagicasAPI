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
    public class StatusAgendamentoController : ControllerBase
    {
        private readonly IStatusAgendamentoRepository _statusAgendamentoRepository;

        public StatusAgendamentoController(IStatusAgendamentoRepository statusAgendamentoRepository)
        {
            _statusAgendamentoRepository = statusAgendamentoRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var statusList = await _statusAgendamentoRepository.GetAllAsync();

            if (!statusList.Any())
                return NotFound();

            var statusDTOs = statusList.Select(s => new StatusAgendamentoOutputDTO
            {   Id = s.Id,
                Nome = s.Nome
            }).ToList();

            return Ok(statusDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var status = await _statusAgendamentoRepository.GetByIdAsync(id);

            if (status == null)
                return NotFound();

            var statusDTO = new StatusAgendamentoInputDTO
            {
                Nome = status.Nome
            };

            return Ok(statusDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] StatusAgendamentoInputDTO statusInputDTO)
        {
            var status = new StatusAgendamento
            {
                Nome = statusInputDTO.Nome
            };

            await _statusAgendamentoRepository.AddAsync(status);

            var statusOutputDTO = new StatusAgendamentoOutputDTO
            {
                Id = status.Id,
                Nome = status.Nome
            };

            return CreatedAtAction(nameof(GetById), new { id = statusOutputDTO.Id }, statusOutputDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] StatusAgendamentoInputDTO statusInputDTO)
        {
            var status = await _statusAgendamentoRepository.GetByIdAsync(id);

            if (status == null)
                return NotFound();

            status.Nome = statusInputDTO.Nome;

            await _statusAgendamentoRepository.UpdateAsync(status);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var status = await _statusAgendamentoRepository.GetByIdAsync(id);
            if (status == null)
                return NotFound();

            await _statusAgendamentoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
