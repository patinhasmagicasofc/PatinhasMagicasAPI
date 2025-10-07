using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface ITipoPagamentoRepository
    {
        Task AddAsync(TipoPagamento tipoPagamento);
        Task<List<TipoPagamento>> GetAllAsync();
        Task<TipoPagamento> GetByIdAsync(int id);
        Task UpdateAsync(TipoPagamento tipoPagamento);
        Task DeleteAsync(int id);
    }
}
