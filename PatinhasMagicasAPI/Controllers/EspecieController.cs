using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecieController : ControllerBase
    {
        private readonly IEspecieRepository _especieRepository;
        private readonly IMapper _mapper;

        public EspecieController(IEspecieRepository especieRepository, IMapper mapper)
        {
            _especieRepository = especieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecieOutputDTO>>> GetAll()
        {
            var usuarioOutputDTOs = await _especieRepository.GetAllAsync();

            return Ok(usuarioOutputDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EspecieOutputDTO>> GetById(int id)
        {
            var usuarioOutputDTO = await _especieRepository.GetByIdAsync(id);

            if (usuarioOutputDTO == null) return NotFound(new { message = "Especie não encontrado." });

            return Ok(usuarioOutputDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post(EspecieInputDTO especieInputDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var especie = _mapper.Map<Especie>(especieInputDTO);

            await _especieRepository.AddAsync(especie);

            return Ok(new { success = true, message = "Especie cadastrado com sucesso!", especieInputDTO });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] EspecieInputDTO especieInputDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var especie = _mapper.Map<Especie>(especieInputDTO);
            especie.Id = id;

            await _especieRepository.UpdateAsync(especie);

            return Ok(new { success = true, message = "Especie atualizado com sucesso!" });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo(int id)
        {
            var tipoServico = await _especieRepository.GetByIdAsync(id);
            if (tipoServico == null)
                return NotFound();

            await _especieRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
