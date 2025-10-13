using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IPedidoRepository
    {
        Task AddAsync(Pedido pedido);
        Task<List<Pedido>> GetAllAsync();
        public IQueryable<Pedido> GetAllPedidos();
        Task<Pedido> GetByIdAsync(int id);
        Task UpdateAsync(Pedido pedido);
        Task DeleteAsync(int id);
    }
}
