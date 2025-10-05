using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IPedidoRepository
    {
        Task AddAsync(Pedido pedido);
        Task<List<Pedido>> GetAllAsync();
        Task<(List<Pedido>, int Total)> GetAllAsync(int page, int pageSize);
        decimal GetTotalVendasHoje(Pedido pedido);
        int GetTotalPedidosHoje(Pedido pedido);
        Task<Pedido> GetByIdAsync(int id);
        Task UpdateAsync(Pedido pedido);
        Task DeleteAsync(int id);
    }
}
