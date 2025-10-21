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
    public class TipoPagamentoController : ControllerBase
    {
        private readonly ITipoPagamentoRepository _tipoPagamentoRepository;
        public TipoPagamentoController(ITipoPagamentoRepository tipopagamentoRepository)
        {
            _tipoPagamentoRepository = tipopagamentoRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var pagamentos = await _tipoPagamentoRepository.GetAllAsync();

            if (!pagamentos.Any())
                return NotFound();

            var tipoPagamentoOutputDTO = pagamentos.Select(p => new TipoPagamentoOutputDTO
            {
                Id = p.Id,
                Nome = p.Nome,
            }).ToList();

            return Ok(tipoPagamentoOutputDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var tipoPagamento = await _tipoPagamentoRepository.GetByIdAsync(id);

            if (tipoPagamento == null)
                return NotFound();

            var tipoPagamentoOutputDTO = new PagamentoOutputDTO
            {
                Id = tipoPagamento.Id,
                Status = tipoPagamento.Nome,
            };

            return Ok(tipoPagamentoOutputDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoPagamentoInputDTO tipoPagamentoInputDTO)
        {
            var tipoPagamento = new TipoPagamento
            {
                Nome = tipoPagamentoInputDTO.Nome,
            };

            await _tipoPagamentoRepository.AddAsync(tipoPagamento);

            var tipoPagamentoOutputDTO = new PagamentoOutputDTO
            {
                Id = tipoPagamento.Id,
                Status = tipoPagamento.Nome,
            };

            return CreatedAtAction(nameof(GetById), new { id = tipoPagamentoOutputDTO.Id }, tipoPagamentoOutputDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoPagamentoInputDTO tipoPagamentoInputDTO)
        {
            var tipoPagamento = await _tipoPagamentoRepository.GetByIdAsync(id);

            if (tipoPagamento == null)
                return NotFound();

            tipoPagamento = new TipoPagamento
            {
                Id = id,
                Nome = tipoPagamentoInputDTO.Nome,
            };

            await _tipoPagamentoRepository.UpdateAsync(tipoPagamento);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tipoPagamento = await _tipoPagamentoRepository.GetByIdAsync(id);
            if (tipoPagamento == null)
                return NotFound();

            await _tipoPagamentoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
