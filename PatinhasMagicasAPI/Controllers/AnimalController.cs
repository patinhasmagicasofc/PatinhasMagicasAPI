using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Services.Interfaces;
using System.Security.Claims;

namespace PatinhasMagicasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _service;

        public AnimalController(IAnimalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }
        private int? GetUsuarioIdLogado()
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return int.TryParse(usuarioIdClaim, out var usuarioId)
                ? usuarioId
                : null;
        }

        [Authorize]
        [HttpGet("meus")]
        public async Task<ActionResult<IEnumerable<AnimalOutputDTO>>> GetMeusAnimais()
        {
            var usuarioId = GetUsuarioIdLogado();

            if (usuarioId is null)
                return Unauthorized(new { message = "Usuario autenticado nao identificado no token." });

            var animais = await _service.GetAllByUsuarioId(usuarioId.Value);
            return Ok(animais);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<AnimalOutputDTO>>> GetAnimaisPorUsuario(int usuarioId)
        {
            var list = await _service.GetAllByUsuarioId(usuarioId);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnimalInputDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usuarioId = GetUsuarioIdLogado();
            if (usuarioId is null)
                return Unauthorized(new { message = "Usuario autenticado nao identificado no token." });

            dto.UsuarioId = usuarioId.Value;

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AnimalInputDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usuarioId = GetUsuarioIdLogado();
            if (usuarioId is null)
                return Unauthorized(new { message = "Usuario autenticado nao identificado no token." });

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            if (existing.UsuarioId != usuarioId.Value)
                return Forbid();

            dto.UsuarioId = usuarioId.Value;

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioId = GetUsuarioIdLogado();
            if (usuarioId is null)
                return Unauthorized(new { message = "Usuario autenticado nao identificado no token." });

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            if (existing.UsuarioId != usuarioId.Value)
                return Forbid();

            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
