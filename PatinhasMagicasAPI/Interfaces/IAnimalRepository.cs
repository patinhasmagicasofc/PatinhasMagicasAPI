using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> GetAllAsync();
        Task<IEnumerable<Animal>> GetAnimalsByUsuarioIdAsync(int usuarioId);
        Task<Animal> GetByIdAsync(int id);
        Task<Animal> AddAsync(Animal animal);
        Task<Animal> UpdateAsync(Animal animal);
        Task<bool> DeleteAsync(int id);
    }
}
