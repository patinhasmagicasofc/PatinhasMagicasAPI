using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IPagamentoRepository
    {
        Task<Pagamento> AddAsync(Pagamento pagamento);
        Task<List<Pagamento>> GetAllAsync();
        Task<Pagamento> GetByIdAsync(int id);
        Task<bool> ExistsByPedidoId(int id);
        Task UpdateAsync(Pagamento pagamento);
        Task DeleteAsync(int id);
    }
}
