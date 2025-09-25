using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IStatusAgendamentoRepository
    {
        Task AddAsync(StatusAgendamento statusAgendamento);
        Task<List<StatusAgendamento>> GetAllAsync();
        Task<StatusAgendamento> GetByIdAsync(int id);
        Task UpdateAsync(StatusAgendamento statusAgendamento);
        Task DeleteAsync(int id);
    }
}
