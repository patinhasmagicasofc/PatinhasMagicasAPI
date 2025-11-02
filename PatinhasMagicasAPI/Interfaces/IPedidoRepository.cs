using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IPedidoRepository
    {
        Task AddAsync(Pedido pedido);
        Task<Pedido> Add(Pedido pedido);
        Task<List<Pedido>> GetAllAsync();
        public IQueryable<Pedido> GetAllPedidos();
        Task<List<Pedido>> GetPedidosByUsuarioId(int id);
        Task<Pedido> GetByIdAsync(int id);
        Task UpdateAsync(Pedido pedido);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int? id);
    }
}
