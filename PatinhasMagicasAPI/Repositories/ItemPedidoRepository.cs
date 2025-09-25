using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class ItemPedidoRepository : IItemPedidoRepository
    {
        private readonly PatinhasMagicasDbContext _context;
        public ItemPedidoRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ItemPedido itemPedido)
        {
            await _context.ItensPedido.AddAsync(itemPedido);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ItemPedido>> GetAllAsync()
        {
            return await _context.ItensPedido.ToListAsync();
        }

        public async Task<ItemPedido> GetByIdAsync(int id)
        {
            return await _context.ItensPedido.FindAsync(id);
        }

        public async Task UpdateAsync(ItemPedido itemPedido)
        {
            _context.ItensPedido.Update(itemPedido);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var itemPedido = await _context.ItensPedido.FindAsync(id);
            if (itemPedido != null)
            {
                _context.ItensPedido.Remove(itemPedido);
                await _context.SaveChangesAsync();
            }
        }
    }
}
