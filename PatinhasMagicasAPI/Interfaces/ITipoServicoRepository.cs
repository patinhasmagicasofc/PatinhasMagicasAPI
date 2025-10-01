using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface ITipoServicoRepository
    {
        Task AddAsync(TipoServico tipoServico);
        Task<List<TipoServico>> GetAllAsync();
        Task<TipoServico> GetByIdAsync(int id);
        Task UpdateAsync(TipoServico tipoServico);
        Task DeleteAsync(int id);
    }
}
