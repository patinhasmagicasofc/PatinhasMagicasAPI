using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Repositories;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IAgendamentoRepository
    {
        Task AddAsync(Agendamento agendamento);
        Task<List<Agendamento>> GetAllAsync();
        Task<Agendamento> GetByIdAsync(int id);
        Task UpdateAsync(Agendamento agendamento);
        Task DeleteAsync(int id);
    }
}
