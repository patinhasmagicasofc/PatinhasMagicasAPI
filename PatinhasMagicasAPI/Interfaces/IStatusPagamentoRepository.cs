using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IStatusPagamentoRepository
    {
        Task<IEnumerable<StatusPagamento>> GetAllAsync();
        public Task<StatusPagamento> GetByNameAsync(string nome);
    }
}
