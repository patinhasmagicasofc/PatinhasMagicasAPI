using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class TipoPagamentoRepository : ITipoPagamentoRepository
    {
        private readonly PatinhasMagicasDbContext _context;
        public TipoPagamentoRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TipoPagamento tipoPagamento)
        {
            await _context.TipoPagamentos.AddAsync(tipoPagamento);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TipoPagamento>> GetAllAsync()
        {
            return await _context.TipoPagamentos.ToListAsync();
        }

        public async Task<TipoPagamento> GetByIdAsync(int id)
        {
            return await _context.TipoPagamentos.FindAsync(id);
        }

        public async Task UpdateAsync(TipoPagamento tipoPagamento)
        {
            _context.TipoPagamentos.Update(tipoPagamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tipoPagamento = await _context.TipoPagamentos.FindAsync(id);
            if (tipoPagamento != null)
            {
                _context.Remove(tipoPagamento);
                await _context.SaveChangesAsync();
            }
        }
    }
}
