using PatinhasMagicasAPI.DTOs;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface IAnimalService
    {
        Task<IEnumerable<AnimalOutputDTO>> GetAllAsync();
        Task<IEnumerable<AnimalOutputDTO>> GetAllByUsuarioId(int usuarioId);
        Task<AnimalOutputDTO> GetByIdAsync(int id);
        Task<AnimalOutputDTO> CreateAsync(AnimalInputDTO dto);
        Task<AnimalOutputDTO> UpdateAsync(int id, AnimalInputDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
