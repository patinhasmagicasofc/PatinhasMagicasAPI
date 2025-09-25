using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Interfaces
{
    public interface IItemPedidoRepository
    {
        Task AddAsync(ItemPedido itemPedido);
        Task<List<ItemPedido>> GetAllAsync();
        Task<ItemPedido> GetByIdAsync(int id);
        Task UpdateAsync(ItemPedido itemPedido);
        Task DeleteAsync(int id);
    }
}
