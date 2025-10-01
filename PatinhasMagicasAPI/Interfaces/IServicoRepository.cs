using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IServicoRepository
    {
        Task AddAsync(Servico servico);
        Task<List<Servico>> GetAllAsync();
        Task<Servico> GetByIdAsync(int id);
        Task UpdateAsync(Servico servico);
        Task DeleteAsync(int id);
    }
}
