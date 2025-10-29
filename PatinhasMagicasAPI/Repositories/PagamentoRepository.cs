using Microsoft.EntityFrameworkCore;
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

        public async Task<Pagamento> AddAsync(Pagamento pagamento)
        {
            await _context.Pagamentos.AddAsync(pagamento);
            await _context.SaveChangesAsync();
            return pagamento;
        }

        public async Task<List<Pagamento>> GetAllAsync()
        {
            return await _context.Pagamentos.ToListAsync();
        }

        public async Task<Pagamento> GetByIdAsync(int id)
        {
            return await _context.Pagamentos.FindAsync(id);
        }

        public async Task<bool> ExistsByPedidoId(int id)
        {
            return await _context.Pagamentos.AnyAsync(p => p.PedidoId == id);
        }

        public async Task UpdateAsync(Pagamento pagamento)
        {
            _context.Pagamentos.Update(pagamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento != null)
            {
                _context.Remove(pagamento);
                await _context.SaveChangesAsync();
            }
        }
    }
}
