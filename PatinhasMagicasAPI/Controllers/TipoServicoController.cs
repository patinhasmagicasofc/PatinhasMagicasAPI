using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
        [ApiController]
        public class TipoServicoController : ControllerBase
        {
            private readonly AppDbContext _context;

            public TipoServicoController(AppDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<TipoServico>>> GetTipos()
            {
                return await _context.TiposServico.ToListAsync();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<TipoServico>> GetTipo(int id)
            {
                var tipo = await _context.TiposServico.FindAsync(id);
                if (tipo == null) return NotFound();
                return tipo;
            }

            [HttpPost]
            public async Task<ActionResult<TipoServico>> PostTipo(TipoServico tipo)
            {
                _context.TiposServico.Add(tipo);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTipo), new { id = tipo.IdTipoServico }, tipo);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> PutTipo(int id, TipoServico tipo)
            {
                if (id != tipo.IdTipoServico) return BadRequest();
                _context.Entry(tipo).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteTipo(int id)
            {
                var tipo = await _context.TiposServico.FindAsync(id);
                if (tipo == null) return NotFound();
                _context.TiposServico.Remove(tipo);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }

