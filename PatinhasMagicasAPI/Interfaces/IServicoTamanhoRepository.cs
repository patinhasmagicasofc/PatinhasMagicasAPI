using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IServicoTamanhoRepository
    {
        Task<ServicoTamanho?> GetByServicoAndTamanhoAsync(int idServico, int idTamanhoAnimal);
    }
}
