using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return Ok(usuarios);
        }

        // GET: api/Usuario/ativos
        // Retorna todos os usuários ativos
        //[HttpGet("ativos")]
        //public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosAtivos()
        //{
        //    var usuariosAtivos = await _usuarioRepository.GetAllAtivosAsync();
        //    return Ok(usuariosAtivos);
        //}

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
        public async Task<ActionResult<Usuario>> Post(Usuario usuario)
        {
            await _usuarioRepository.AddAsync(usuario);

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }

        // PUT: api/Usuario/5
        // Atualiza um usuário existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PostUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest("O ID na URL não corresponde ao ID do objeto.");
            }

            await _usuarioRepository.UpdateAsync(usuario);
            return NoContent();
        }

        // PUT: api/Usuario/inativar/5
        // Inativa um usuário (exclusão lógica)
        [HttpPut("inativar/{id}")]
        public async Task<IActionResult> InativarUsuario(int id)
        {
            await _usuarioRepository.InativarAsync(id);
            return NoContent();
        }

        // PUT: api/Usuario/reativar/5
        // Reativa um usuário inativo
        [HttpPut("reativar/{id}")]
        public async Task<IActionResult> ReativarUsuario(int id)
        {
            await _usuarioRepository.ReativarAsync(id);
            return NoContent();
        }
    }
}
