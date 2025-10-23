using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IEspecieRepository
    {
        Task AddAsync(Especie especie);
        Task<List<Especie>> GetAllAsync();
        Task<Especie> GetByIdAsync(int id);
        Task UpdateAsync(Especie especie);
        Task DeleteAsync(int id);
    }
}
