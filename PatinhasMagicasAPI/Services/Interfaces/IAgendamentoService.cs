using PatinhasMagicasAPI.DTOs;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface IAgendamentoService
    {
        Task CriarAsync(AgendamentoInputDTO dto);
        Task<IEnumerable<AgendamentoOutputDTO>> ListarAsync();
        Task<AgendamentoOutputDTO> BuscarPorIdAsync(int id);
        Task DeletarAsync(int id);
    }
}
