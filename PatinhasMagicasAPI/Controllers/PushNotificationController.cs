using Microsoft.AspNetCore.Mvc;
using PatinhasMagicasAPI.DTOs;
using PatinhasMagicasAPI.Services.Interfaces;

namespace PatinhasMagicasAPI.Controllers
{
    [ApiController]
    [Route("api/push-notifications")]
    public class PushNotificationController : ControllerBase
    {
        private readonly IPushNotificationService _pushNotificationService;

        public PushNotificationController(IPushNotificationService pushNotificationService)
        {
            _pushNotificationService = pushNotificationService;
        }

        [HttpGet("vapid-public-key")]
        public ActionResult<PushPublicKeyOutputDTO> GetPublicKey()
        {
            return Ok(new PushPublicKeyOutputDTO
            {
                PublicKey = _pushNotificationService.GetPublicKey()
            });
        }

        [HttpPost("subscriptions")]
        public async Task<IActionResult> SaveSubscription([FromBody] PushSubscriptionInputDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _pushNotificationService.SaveSubscriptionAsync(dto);
            return Ok();
        }

        [HttpDelete("subscriptions")]
        public async Task<IActionResult> RemoveSubscription([FromBody] PushSubscriptionRemoveDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _pushNotificationService.RemoveSubscriptionAsync(dto.Endpoint);
            return NoContent();
        }

        [HttpPost("test/{usuarioId:int}")]
        public async Task<IActionResult> SendTestNotification(int usuarioId)
        {
            await _pushNotificationService.SendAsync(usuarioId, new PushNotificationRequestDTO
            {
                Title = "Patinhas Magicas",
                Body = "Sua notificacao push esta funcionando.",
                Url = "/perfil"
            });

            return Ok(new { success = true });
        }
    }
}
