using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatinhasMagicasAPI.Controllers
{
    // Define a rota e marca a classe como um controlador de API
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

        // Injeção de dependência do repositório
        public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository)
        {
            _tipoUsuarioRepository = tipoUsuarioRepository;
        }

        // GET: api/TipoUsuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoUsuario>>> GetTiposUsuario()
        {
            var tipos = await _tipoUsuarioRepository.GetAllAsync();
            return Ok(tipos);
        }

        // GET: api/TipoUsuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUsuario>> GetTipoUsuario(int id)
        {
            var tipo = await _tipoUsuarioRepository.GetByIdAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }
            return Ok(tipo);
        }

        // POST: api/TipoUsuario
        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> PostTipoUsuario(TipoUsuario tipo)
        {
            await _tipoUsuarioRepository.AddAsync(tipo);
            return CreatedAtAction(nameof(GetTipoUsuario), new { id = tipo.Id }, tipo);
        }

        // PUT: api/TipoUsuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoUsuario(int id, TipoUsuario tipo)
        {
            if (id != tipo.Id)
            {
                return BadRequest();
            }

            var existingTipo = await _tipoUsuarioRepository.GetByIdAsync(id);
            if (existingTipo == null)
            {
                return NotFound();
            }

            await _tipoUsuarioRepository.UpdateAsync(tipo);
            return NoContent();
        }

        // DELETE: api/TipoUsuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoUsuario(int id)
        {
            var tipo = await _tipoUsuarioRepository.GetByIdAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }

            await _tipoUsuarioRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}