using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly PatinhasMagicasDbContext _context;

        public AnimalRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task<Animal> AddAsync(Animal animal)
        {
            _context.Animais.Add(animal);
            await _context.SaveChangesAsync();
            return animal;
        }

        public async Task<IEnumerable<Animal>> GetAnimalsByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Animais
                .Include(a => a.TamanhoAnimal)
                .Include(a => a.Especie)
                .Where(a => a.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Animais.FindAsync(id);
            if (entity == null) return false;

            _context.Animais.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Animal>> GetAllAsync()
        {
            return await _context.Animais
                .Include(a => a.TamanhoAnimal)
                .Include(a => a.Especie)
                .Include(a => a.Usuario)
                .ToListAsync();
        }

        public async Task<Animal> GetByIdAsync(int id)
        {
            return await _context.Animais
                .Include(a => a.TamanhoAnimal)
                .Include(a => a.Usuario)
                .Include(a => a.Especie)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Animal> UpdateAsync(Animal animal)
        {
            _context.Animais.Update(animal);
            await _context.SaveChangesAsync();
            return animal;
        }
    }
}