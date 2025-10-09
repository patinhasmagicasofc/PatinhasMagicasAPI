using Microsoft.AspNetCore.Authorization;
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
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;

        public LoginController(IUsuarioService usuarioService, ITokenService tokenService)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioInputDTO loginUsuarioInputDTO)
        {

            var loginUsuarioOutputDTO = await _usuarioService.ValidarLoginAsync(loginUsuarioInputDTO.Email, loginUsuarioInputDTO.Senha);

            if (loginUsuarioOutputDTO == null)return Unauthorized(new { Message = "Usuário ou senha inválidos ou inativos." });

            var usuario = new Usuario
            {
                IdUsuario = loginUsuarioOutputDTO.Id,
                Email = loginUsuarioOutputDTO.Email,
                TipoUsuario = loginUsuarioOutputDTO.TipoUsuario
            };

            // 2. Determina o perfil do usuário (Role Claim)
            string role = usuario.TipoUsuario?.DescricaoTipoUsuario ?? "Cliente";

            // 3. Gera o Token JWT
            var token = _tokenService.GenerateToken(usuario);


            // 4. Retorna a resposta de sucesso
            return Ok(new
            {
                Token = token,
                Perfil = role,
                IdUsuario = loginUsuarioOutputDTO.Id
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