using PatinhasMagicasAPI.DTOs;

namespace PatinhasMagicasAPI.Services.Interfaces
{
    public interface IAgendamentoService
    {
        Task<AgendamentoOutputDTO> CriarAgendamentoAsync(AgendamentoCreateDTO agendamentoCreateDTO);
        Task<AgendamentoDetalhesDTO> GetByIdAsync(int id);
        Task<IEnumerable<AgendamentoDetalhesDTO>> GetAllAsync();
        Task DeletarAsync(int id);
    }
}
