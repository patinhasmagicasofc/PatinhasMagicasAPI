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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(UsuarioInputDTO usuarioInputDTO)
        {
            var usuario = new Usuario
            {
                Nome = usuarioInputDTO.Nome,
                Email = usuarioInputDTO.Email,
                CPF = usuarioInputDTO.CPF,
                Ddd = usuarioInputDTO.Ddd,
                Telefone = usuarioInputDTO.Telefone,
                DataCadastro = DateTime.Now,
                Senha = usuarioInputDTO.Senha,
                TipoUsuarioId = usuarioInputDTO.TipoUsuarioId
            };

            await _usuarioRepository.AddAsync(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, UsuarioInputDTO usuarioInputDTO)
        {
            var usuario = new Usuario
            {
                IdUsuario = id,
                Nome = usuarioInputDTO.Nome,
                Email = usuarioInputDTO.Email,
                CPF = usuarioInputDTO.CPF,
                Ddd = usuarioInputDTO.Ddd,
                Telefone = usuarioInputDTO.Telefone,
                Senha = usuarioInputDTO.Senha,
                TipoUsuarioId = usuarioInputDTO.TipoUsuarioId
            };

            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);
            if (existingUsuario == null)
            {
                return NotFound();
            }

            await _usuarioRepository.UpdateAsync(usuario);
            return NoContent();
        }

        [HttpPut("inativar/{id}")]
        public async Task<IActionResult> InativarUsuario(int id)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);
            if (existingUsuario == null)
            {
                return NotFound();
            }

            await _usuarioRepository.InativarAsync(id);
            return NoContent();
        }

        [HttpPut("reativar/{id}")]
        public async Task<IActionResult> ReativarUsuario(int id)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);
            if (existingUsuario == null)
            {
                return NotFound();
            }

            await _usuarioRepository.ReativarAsync(id);
            return NoContent();
        }
    }
}