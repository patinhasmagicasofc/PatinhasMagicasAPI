using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class PushSubscriptionRepository : IPushSubscriptionRepository
    {
        private readonly PatinhasMagicasDbContext _context;

        public PushSubscriptionRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task<PushSubscription?> GetByEndpointAsync(string endpoint)
        {
            return await _context.PushSubscriptions
                .FirstOrDefaultAsync(subscription => subscription.Endpoint == endpoint);
        }

        public async Task<List<PushSubscription>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.PushSubscriptions
                .Where(subscription => subscription.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task AddAsync(PushSubscription pushSubscription)
        {
            await _context.PushSubscriptions.AddAsync(pushSubscription);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PushSubscription pushSubscription)
        {
            _context.PushSubscriptions.Update(pushSubscription);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PushSubscription pushSubscription)
        {
            _context.PushSubscriptions.Remove(pushSubscription);
            await _context.SaveChangesAsync();
        }
    }
}
