using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IAgendamentoServicoRepository
    {

        Task<List<AgendamentoServico>> GetAllAsync();
        Task<AgendamentoServico> GetByIdAsync(int id);
        Task<List<AgendamentoServico>> GetServicosByAgendamentoIdAsync(int agendamentoId);
        Task AddAsync(AgendamentoServico agendamentoServico);
        Task UpdateAsync(AgendamentoServico agendamentoServico);
        Task DeleteAsync(int id);
    }
}
