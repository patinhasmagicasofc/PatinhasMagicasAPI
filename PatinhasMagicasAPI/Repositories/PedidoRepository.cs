using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly PatinhasMagicasDbContext _context;

        public PedidoRepository(PatinhasMagicasDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Pedido pedido)
        {
            await _context.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Pedido>> GetAllAsync()
        {
            return await _context.Pedidos.Include(p => p.Usuario).ThenInclude(c => c.Endereco)
                                         .Include(p => p.ItensPedido).ThenInclude(i => i.Produto)
                                         .Include(p => p.Pagamentos).ThenInclude(pg => pg.StatusPagamento)
                                         .Include(p => p.Pagamentos).ThenInclude(pg => pg.TipoPagamento)
                                         .Include(p => p.StatusPedido).ToListAsync();
        }

        public IQueryable<Pedido> GetAllPedidos()
        {
            return _context.Pedidos
                           .Include(p => p.Usuario).ThenInclude(c => c.Endereco)
                           .Include(p => p.ItensPedido).ThenInclude(i => i.Produto)
                           .Include(p => p.Pagamentos).ThenInclude(pg => pg.StatusPagamento)
                           .Include(p => p.Pagamentos).ThenInclude(pg => pg.TipoPagamento)
                           .Include(p => p.StatusPedido)
                           .AsQueryable();
        }

        public async Task<Pedido> GetByIdAsync(int id)
        {
            return await _context.Pedidos.Include(p => p.Usuario).ThenInclude(p => p.Endereco)
                                         .Include(p => p.ItensPedido).ThenInclude(p => p.Produto)
                                         .Include(p => p.Pagamentos).ThenInclude(p => p.StatusPagamento)
                                         .Include(p => p.StatusPedido).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Pedido pedido)
        {
            _context.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }
    }
}
