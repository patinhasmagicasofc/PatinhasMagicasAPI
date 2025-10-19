using PatinhasMagicasAPI.DTOs;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface IServicoService
    {
        Task<IEnumerable<ServicoOutputDTO>> GetServicosPorAnimalAsync(int idAnimal);

    }
}
