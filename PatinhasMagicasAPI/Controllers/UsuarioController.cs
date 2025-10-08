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
            // Verifica se já existe email e cpf
            var emailExistente = await _usuarioRepository.GetByEmailAsync(usuarioInputDTO.Email);
            if (emailExistente != null)
                return BadRequest(new { mensagem = "E-mail já cadastrado." });

            var cpfExistente = await _usuarioRepository.GetByCPFAsync(usuarioInputDTO.CPF);
            if (cpfExistente != null)
                return BadRequest(new { mensagem = "CPF já cadastrado." });


            var usuario = new Usuario
            {
                Nome = usuarioInputDTO.Nome,
                Email = usuarioInputDTO.Email,
                CPF = usuarioInputDTO.CPF,
                Ddd = usuarioInputDTO.Ddd.Value,
                Telefone = usuarioInputDTO.Telefone,
                Senha = usuarioInputDTO.Senha,
                TipoUsuarioId = usuarioInputDTO.TipoUsuarioId.Value
            };

            await _usuarioRepository.AddAsync(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioInputDTO dto)
        {

            var emailDuplicado = await _usuarioRepository.GetByEmailAsync(dto.Email);
            if (emailDuplicado != null && emailDuplicado.IdUsuario != id)
                return BadRequest(new { mensagem = "E-mail já cadastrado para outro usuário." });


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound();

            // Atualiza apenas campos enviados
            if (!string.IsNullOrWhiteSpace(dto.Nome))
                usuario.Nome = dto.Nome;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                usuario.Email = dto.Email;

            if (dto.Ddd.HasValue)
                usuario.Ddd = dto.Ddd.Value;

            if (!string.IsNullOrWhiteSpace(dto.Telefone))
                usuario.Telefone = dto.Telefone;

            if (dto.TipoUsuarioId.HasValue)
                usuario.TipoUsuarioId = dto.TipoUsuarioId.Value;

            if (!string.IsNullOrWhiteSpace(dto.Senha))
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha);


            await _usuarioRepository.UpdateAsync(usuario);

            return Ok(usuario);
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