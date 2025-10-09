using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioOutputDTO>>> GetUsuarios()
        {
            var usuarioOutputDTOs = await _usuarioService.GetAllAsync();

            return Ok(usuarioOutputDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioOutputDTO>> GetUsuario(int id)
        {
            var usuarioOutputDTO = await _usuarioService.GetByIdAsync(id);

            if (usuarioOutputDTO == null) return NotFound(new { message = "Usuario não encontrado." });

            return Ok(usuarioOutputDTO);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(UsuarioInputDTO usuarioInputDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _usuarioService.AddAsync(usuarioInputDTO);

            //return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioInputDTO usuarioInputDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _usuarioService.UpdateAsync(id, usuarioInputDTO);
            return NoContent();
        }

        [HttpPut("inativar/{id}")]
        public async Task<IActionResult> InativarUsuario(int id)
        {
            await _usuarioService.InativarAsync(id);
            return NoContent();
        }

        [HttpPut("reativar/{id}")]
        public async Task<IActionResult> ReativarUsuario(int id)
        {
            await _usuarioService.ReativarAsync(id);
            return NoContent();
        }
    }
}