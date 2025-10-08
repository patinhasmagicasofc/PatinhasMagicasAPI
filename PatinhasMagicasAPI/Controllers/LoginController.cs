using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;
using System.Threading.Tasks;

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
            // 1. Valida as credenciais. A lógica de HASH e busca por e-mail 
            // está agora implementada de forma segura no UsuarioRepository.
            var usuario = await _usuarioRepository.ValidarLoginAsync(model.Email, model.Senha);

            if (usuario == null)
            {
                // Retorno padronizado de erro da API
                return Unauthorized(new { Message = "Usuário ou senha inválidos ou inativos." });
            }

            // 2. Determina o perfil do usuário (Role Claim)
            string role = usuario.TipoUsuario?.DescricaoTipoUsuario ?? "Cliente";

            // 3. Gera o Token JWT
            var token = _tokenService.GenerateToken(usuario);


            // 4. Retorna a resposta de sucesso
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
            // O Logout é tipicamente tratado no cliente (deletando o token JWT).
            // Este endpoint é mais informativo.
            return Ok(new { Message = "Sessão encerrada com sucesso no lado do cliente." });
        }
    }
}