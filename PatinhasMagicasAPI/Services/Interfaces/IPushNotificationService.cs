using PatinhasMagicasAPI.DTOs;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface IPushNotificationService
    {
        string GetPublicKey();
        Task SaveSubscriptionAsync(PushSubscriptionInputDTO dto);
        Task RemoveSubscriptionAsync(string endpoint);
        Task SendAsync(int usuarioId, PushNotificationRequestDTO notification);
    }
}
