using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class TipoServicoRepository : ITipoServicoRepository
    {
        private readonly PatinhasMagicasDbContext _context;
        public TipoServicoRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TipoServico tipoServico)
        {
            await _context.TiposServico.AddAsync(tipoServico);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TipoServico>> GetAllAsync()
        {
            return await _context.TiposServico.ToListAsync();
        }

        public async Task<TipoServico> GetByIdAsync(int id)
        {
            return await _context.TiposServico.FindAsync(id);
        }

        public async Task UpdateAsync(TipoServico tipoServico)
        {
            _context.TiposServico.Update(tipoServico);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var tipoServico = await _context.TiposServico.FindAsync(id);
            if (tipoServico != null)
            {
                _context.TiposServico.Remove(tipoServico);
                await _context.SaveChangesAsync();
            }
        }
    }
}
