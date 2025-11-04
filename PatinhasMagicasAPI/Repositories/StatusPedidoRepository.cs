using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class StatusPedidoRepository : IStatusPedidoRepository
    {
        private readonly PatinhasMagicasDbContext _context;

        public StatusPedidoRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task<StatusPedido?> GetByNameAsync(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return null;

            return await _context.StatusPedido
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Nome.ToLower() == nome.ToLower());
        }
    }
}
