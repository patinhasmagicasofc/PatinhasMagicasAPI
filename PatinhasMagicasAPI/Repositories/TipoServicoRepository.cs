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
            await _context.TipoServico.AddAsync(tipoServico);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TipoServico>> GetAllAsync()
        {
            return await _context.TipoServico.ToListAsync();
        }

        public async Task<TipoServico> GetByIdAsync(int id)
        {
            return await _context.TipoServico.FindAsync(id);
        }

        public async Task UpdateAsync(TipoServico tipoServico)
        {
            _context.TipoServico.Update(tipoServico);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var tipoServico = await _context.TipoServico.FindAsync(id);
            if (tipoServico != null)
            {
                _context.TipoServico.Remove(tipoServico);
                await _context.SaveChangesAsync();
            }
        }
    }
}
