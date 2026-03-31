using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IPushSubscriptionRepository
    {
        Task<PushSubscription?> GetByEndpointAsync(string endpoint);
        Task<List<PushSubscription>> GetByUsuarioIdAsync(int usuarioId);
        Task AddAsync(PushSubscription pushSubscription);
        Task UpdateAsync(PushSubscription pushSubscription);
        Task DeleteAsync(PushSubscription pushSubscription);
    }
}
