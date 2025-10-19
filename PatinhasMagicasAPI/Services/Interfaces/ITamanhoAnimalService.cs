using PatinhasMagicasAPI.DTOs;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface ITamanhoAnimalService
    {
        Task<IEnumerable<TamanhoAnimalOutputDTO>> GetAllAsync();
        Task<TamanhoAnimalOutputDTO> GetByIdAsync(int id);
        Task<TamanhoAnimalOutputDTO> CreateAsync(TamanhoAnimalInputDTO dto);
        Task<TamanhoAnimalOutputDTO> UpdateAsync(int id, TamanhoAnimalInputDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
