using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public LoginController(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        [HttpPost("login")] 
        [AllowAnonymous]

        public async Task<IActionResult> Login([FromBody] LoginUsuario model)
        {

            var usuario = await _usuarioRepository.ValidarLoginAsync(model.Email, model.Senha);

            if (usuario == null)
            {
                // Retorno padronizado de erro da API
                return Unauthorized(new { Message = "Usuário ou senha inválidos ou inativos." });
            }

            string role = usuario.TipoUsuario?.DescricaoTipoUsuario ?? "Cliente";

            var token = _tokenService.GenerateToken(usuario);


            return Ok(new
            {
                Token = token,
                Perfil = role,
                IdUsuario = usuario.IdUsuario
            });
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {

            return Ok(new { Message = "Sessão encerrada com sucesso no lado do cliente." });
        }
    }
}