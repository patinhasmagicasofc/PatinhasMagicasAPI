using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly PatinhasMagicasDbContext _context;
        public PagamentoRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Pagamento pagamento)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Pagamento>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Pagamento> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Pagamento pagamento)
        {
            throw new NotImplementedException();
        }
    }
}
