using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly PatinhasMagicasDbContext _context;
        public ServicoRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Servico servico)
        {
            await _context.Servicos.AddAsync(servico);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Servico>> GetAllAsync()
        {
            return await _context.Servicos.ToListAsync();
        }

        public async Task<Servico> GetByIdAsync(int id)
        {
            return await _context.Servicos.FindAsync(id);
        }

        public async Task UpdateAsync(Servico servico)
        {
            _context.Servicos.Update(servico);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico != null)
            {
                _context.Servicos.Remove(servico);
                await _context.SaveChangesAsync();
            }
        }
    }
}
