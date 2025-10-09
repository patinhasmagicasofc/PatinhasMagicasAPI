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
    public class TipoServicoController : ControllerBase
    {
        private readonly ITipoServicoRepository _tipoServicoRepository;

        public TipoServicoController(ITipoServicoRepository tipoServicoRepository)
        {
            _tipoServicoRepository = tipoServicoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoServicoOutputDTO>>> GetTipos()
        {
            var tipoServicos = await _tipoServicoRepository.GetAllAsync();

            if (!tipoServicos.Any())
                return NotFound();

            var tipoServicosDTO = tipoServicos.Select(p => new TipoServicoOutputDTO
            {
                Id = p.Id,
                Nome = p.Nome,
            }).ToList();

            return Ok(tipoServicosDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoServicoOutputDTO>> GetById(int id)
        {
            var tipoServico = await _tipoServicoRepository.GetByIdAsync(id);

            if (tipoServico == null)
                return NotFound();

            var tipoServicoDTO = new TipoServicoOutputDTO
            {
                Id = tipoServico.Id,
                Nome = tipoServico.Nome,
            };

            return Ok(tipoServicoDTO);
        }

        [HttpPost]
        public async Task<ActionResult<TipoServicoOutputDTO>> PostTipo(TipoServicoInputDTO tipoServicoInputDTO)
        {
            var tipoServico = new TipoServico
            {
                Nome = tipoServicoInputDTO.Nome,
            };

            await _tipoServicoRepository.AddAsync(tipoServico);

            var tipoServicoOutputDTO = new TipoServicoOutputDTO
            {
                Id = tipoServico.Id,
                Nome = tipoServico.Nome,
            };

            return CreatedAtAction(nameof(GetById), new { id = tipoServicoOutputDTO.Id }, tipoServicoOutputDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo(int id, TipoServicoInputDTO tipoServicoInputDTO)
        {
            var tipoServico = await _tipoServicoRepository.GetByIdAsync(id);

            if (tipoServico == null)
                return NotFound();

            tipoServico = new TipoServico
            {
                Id = id,
                Nome = tipoServicoInputDTO.Nome
            };

            await _tipoServicoRepository.UpdateAsync(tipoServico);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo(int id)
        {
            var tipoServico = await _tipoServicoRepository.GetByIdAsync(id);
            if (tipoServico == null)
                return NotFound();

            await _tipoServicoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

