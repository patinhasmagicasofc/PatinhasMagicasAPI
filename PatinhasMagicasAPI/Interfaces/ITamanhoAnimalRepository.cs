using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface ITamanhoAnimalRepository
    {
        Task<IEnumerable<TamanhoAnimal>> GetAllAsync();
        Task<TamanhoAnimal> GetByIdAsync(int id);
        Task<TamanhoAnimal> AddAsync(TamanhoAnimal tamanhoAnimal);
        Task<TamanhoAnimal> UpdateAsync(TamanhoAnimal tamanhoAnimal);
        Task<bool> DeleteAsync(int id);
    }
}
