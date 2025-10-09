using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services;

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

        // GET: api/Usuario
        // Retorna todos os usuários
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioOutputDTO>>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            var usuariosDTO = usuarios.Select(u => new UsuarioOutputDTO
            {
                Id = u.IdUsuario,
                Nome = u.Nome,
                Email = u.Email,
                CPF = u.CPF,
                Ddd = u.Ddd,
                Telefone = u.Telefone,
                DataCadastro = u.DataCadastro,
                Senha = u.Senha,
                TipoUsuarioId = u.TipoUsuarioId,
                TipoUsuarioNome = u.TipoUsuario.DescricaoTipoUsuario
            }).ToList();

            return Ok(usuariosDTO);
        }

        // GET: api/Usuario/5
        // Retorna um usuário específico pelo ID
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

        // POST: api/Usuario
        // Adiciona um novo usuário
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

        // PUT: api/Usuario/5
        // Atualiza um usuário existente
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
                DataCadastro = DateTime.Now,
                Senha = usuarioInputDTO.Senha,
                TipoUsuarioId = usuarioInputDTO.TipoUsuarioId
            };

            //if (id != usuario.IdUsuario)
            //{
            //    return BadRequest("O ID na URL não corresponde ao ID do objeto.");
            //}

            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);
            if (existingUsuario == null)
            {
                return NotFound();
            }

            await _usuarioRepository.UpdateAsync(usuario);
            return NoContent();
        }

        // PUT: api/Usuario/inativar/5
        // Inativa um usuário (exclusão lógica)
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

        // PUT: api/Usuario/reativar/5
        // Reativa um usuário inativo
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