using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Services.Interfaces;
using System.Security.Claims;

namespace PatinhasMagicasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasskeysController : ControllerBase
    {
        private readonly IPasskeyService _passkeyService;

        public PasskeysController(IPasskeyService passkeyService)
        {
            _passkeyService = passkeyService;
        }

        [Authorize]
        [HttpPost("register/options")]
        public async Task<IActionResult> BeginRegistration()
        {
            var usuarioId = GetUsuarioIdLogado();

            if (usuarioId is null)
            {
                return Unauthorized(new { message = "Usuario autenticado nao identificado no token." });
            }

            var options = await _passkeyService.BeginRegistrationAsync(usuarioId.Value);
            return Ok(options);
        }

        [Authorize]
        [HttpPost("register/complete")]
        public async Task<IActionResult> CompleteRegistration([FromBody] PasskeyCompleteRequestDTO request)
        {
            var usuarioId = GetUsuarioIdLogado();

            if (usuarioId is null)
            {
                return Unauthorized(new { message = "Usuario autenticado nao identificado no token." });
            }

            var result = await _passkeyService.CompleteRegistrationAsync(usuarioId.Value, request);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login/options")]
        public async Task<IActionResult> BeginAuthentication()
        {
            var options = await _passkeyService.BeginAuthenticationAsync();
            return Ok(options);
        }

        [AllowAnonymous]
        [HttpPost("login/complete")]
        public async Task<IActionResult> CompleteAuthentication([FromBody] PasskeyCompleteRequestDTO request)
        {
            var loginUsuarioOutputDTO = await _passkeyService.CompleteAuthenticationAsync(request);
            return Ok(new { success = true, message = "Login com biometria efetuado com sucesso!", data = loginUsuarioOutputDTO });
        }

        [Authorize]
        [HttpGet("mine")]
        public async Task<IActionResult> GetMine()
        {
            var usuarioId = GetUsuarioIdLogado();

            if (usuarioId is null)
            {
                return Unauthorized(new { message = "Usuario autenticado nao identificado no token." });
            }

            var credentials = await _passkeyService.GetByUsuarioAsync(usuarioId.Value);
            return Ok(credentials);
        }

        [Authorize]
        [HttpDelete("{credentialId:int}")]
        public async Task<IActionResult> Delete(int credentialId)
        {
            var usuarioId = GetUsuarioIdLogado();

            if (usuarioId is null)
            {
                return Unauthorized(new { message = "Usuario autenticado nao identificado no token." });
            }

            await _passkeyService.RemoveAsync(usuarioId.Value, credentialId);
            return NoContent();
        }

        private int? GetUsuarioIdLogado()
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return int.TryParse(usuarioIdClaim, out var usuarioId)
                ? usuarioId
                : null;
        }
    }
}
