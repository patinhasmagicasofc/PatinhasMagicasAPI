using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class EspecieRepository : IEspecieRepository
    {
        private readonly PatinhasMagicasDbContext _context;
        public EspecieRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Especie especie)
        {
            _context.Especies.AddAsync(especie);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Especie>> GetAllAsync()
        {
            return await _context.Especies.ToListAsync();
        }

        public async Task<Especie> GetByIdAsync(int id)
        {
            return await _context.Especies.FindAsync(id);
        }

        public async Task UpdateAsync(Especie especie)
        {
            _context.Especies.Update(especie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var especie = await _context.Especies.FindAsync(id);
            if (especie != null)
            {
                _context.Remove(especie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
