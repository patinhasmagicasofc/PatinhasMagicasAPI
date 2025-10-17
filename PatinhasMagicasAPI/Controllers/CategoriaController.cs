using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetTiposUsuario()
        {
            var tipos = await _categoriaRepository.GetAllAsync();
            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetTipoUsuario(int id)
        {
            var tipo = await _categoriaRepository.GetByIdAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }
            return Ok(tipo);
        }

        // POST: api/TipoUsuario
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostTipoUsuario(Categoria tipo)
        {
            await _categoriaRepository.AddAsync(tipo);
            return CreatedAtAction(nameof(GetTipoUsuario), new { id = tipo.Id }, tipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoUsuario(int id, Categoria tipo)
        {
            if (id != tipo.Id)
            {
                return BadRequest();
            }

            var existingTipo = await _categoriaRepository.GetByIdAsync(id);
            if (existingTipo == null)
            {
                return NotFound();
            }

            await _categoriaRepository.UpdateAsync(tipo);
            return NoContent();
        }

   
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoUsuario(int id)
        {
            var tipo = await _categoriaRepository.GetByIdAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }

            await _categoriaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}