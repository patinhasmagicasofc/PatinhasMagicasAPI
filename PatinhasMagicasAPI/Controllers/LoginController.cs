using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioInputDTO loginUsuarioInputDTO)
        {

            var loginUsuarioOutputDTO = await _usuarioService.ValidarLoginAsync(loginUsuarioInputDTO.Email, loginUsuarioInputDTO.Senha);

            if (loginUsuarioOutputDTO == null)return Unauthorized(new { Message = "Usuário ou senha inválidos ou inativos." });

            return Ok(new { success = true, message = "Login Efetuado com sucesso sucesso!", data = loginUsuarioOutputDTO });
        }
    }
}