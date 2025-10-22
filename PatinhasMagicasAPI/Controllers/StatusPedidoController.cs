using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class StatusPedidoController : ControllerBase
    {
        private readonly PatinhasMagicasDbContext _context;

        public StatusPedidoController(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusPedido>>> GetAllAsync()
        {
            return await _context.StatusPedido.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusPedido>> GetStatusPedidoOutputDTO(int id)
        {
            var statusPedidoOutputDTO = await _context.StatusPedido.FindAsync(id);

            if (statusPedidoOutputDTO == null)
            {
                return NotFound();
            }

            return statusPedidoOutputDTO;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusPedidoOutputDTO(int id, StatusPedido statusPedidoOutputDTO)
        {
            if (id != statusPedidoOutputDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(statusPedidoOutputDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusPedidoOutputDTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StatusPedido
        [HttpPost]
        public async Task<ActionResult<StatusPedidoOutputDTO>> PostStatusPedidoOutputDTO(StatusPedido statusPedidoOutputDTO)
        {
            _context.StatusPedido.Add(statusPedidoOutputDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatusPedidoOutputDTO", new { id = statusPedidoOutputDTO.Id }, statusPedidoOutputDTO);
        }

        // DELETE: api/StatusPedido/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusPedidoOutputDTO(int id)
        {
            var statusPedidoOutputDTO = await _context.StatusPedido.FindAsync(id);
            if (statusPedidoOutputDTO == null)
            {
                return NotFound();
            }

            _context.StatusPedido.Remove(statusPedidoOutputDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusPedidoOutputDTOExists(int id)
        {
            return _context.StatusPedido.Any(e => e.Id == id);
        }
    }
}
