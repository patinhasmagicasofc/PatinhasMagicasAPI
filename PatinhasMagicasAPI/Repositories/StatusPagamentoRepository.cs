using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class StatusPagamentoRepository : IStatusPagamentoRepository
    {
        private readonly PatinhasMagicasDbContext _context;

        public StatusPagamentoRepository(PatinhasMagicasDbContext dbContext)
        {
            _context = dbContext;
        }

        public Task<IEnumerable<StatusPagamento>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<StatusPagamento> GetByNameAsync(string nome)
        {
            return await _context.StatusPagamento.FirstOrDefaultAsync(s => s.Nome == nome);
        }
    }
}
