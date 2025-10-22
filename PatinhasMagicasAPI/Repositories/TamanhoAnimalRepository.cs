using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class TamanhoAnimalRepository : ITamanhoAnimalRepository
    {
        private readonly PatinhasMagicasDbContext _context;

        public TamanhoAnimalRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task<TamanhoAnimal> AddAsync(TamanhoAnimal tamanhoAnimal)
        {
            _context.TamanhoAnimal.Add(tamanhoAnimal);
            await _context.SaveChangesAsync();
            return tamanhoAnimal;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TamanhoAnimal.FindAsync(id);
            if (entity == null) return false;

            _context.TamanhoAnimal.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TamanhoAnimal>> GetAllAsync()
        {
            return await _context.TamanhoAnimal.ToListAsync();
        }

        public async Task<TamanhoAnimal> GetByIdAsync(int id)
        {
            return await _context.TamanhoAnimal.FindAsync(id);
        }

        public async Task<TamanhoAnimal> UpdateAsync(TamanhoAnimal tamanhoAnimal)
        {
            _context.TamanhoAnimal.Update(tamanhoAnimal);
            await _context.SaveChangesAsync();
            return tamanhoAnimal;
        }
    }
}