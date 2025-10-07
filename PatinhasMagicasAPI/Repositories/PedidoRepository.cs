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

        public int GetTotalPedidosHoje(Pedido pedido)
        {
            var hoje = DateTime.Today;
            var totalPedidosHoje = _context.Pedidos
                .Count(p => p.DataPedido.Date == hoje);

            return totalPedidosHoje;
        }

        public decimal GetTotalVendasHoje(Pedido pedido)
        {
            var hoje = DateTime.Today;
            var totalVendasHoje = _context.Pedidos
                                                .Where(p => p.DataPedido.Date == hoje && p.StatusPedido.Nome == "Pago")
                                                .Sum(p => p.ItensPedido.Sum(i => i.PrecoUnitario * i.Quantidade));

            return totalVendasHoje;
        }

        public async Task<List<Pedido>> GetAllAsync()
        {
            return await _context.Pedidos.Include(p => p.Cliente).ThenInclude(c => c.Endereco)
                                         .Include(p => p.ItensPedido).ThenInclude(i => i.Produto)
                                         .Include(p => p.Pagamentos).ThenInclude(pg => pg.StatusPagamento)
                                         .Include(p => p.Pagamentos).ThenInclude(pg => pg.TipoPagamento)
                                         .Include(p => p.StatusPedido).ToListAsync();
        }

        public async Task<(List<Pedido>, int Total)> GetAllAsync(int page, int pageSize, DateTime dataInicio, DateTime dataFim)
        {
            var skip = (page - 1) * pageSize;
            var query = _context.Pedidos
                                        .Where(p => p.DataPedido.Date >= dataInicio && p.DataPedido.Date <= dataFim)
                                        .Include(p => p.Cliente).ThenInclude(c => c.Endereco)
                                        .Include(p => p.ItensPedido).ThenInclude(i => i.Produto)
                                        .Include(p => p.Pagamentos).ThenInclude(pg => pg.StatusPagamento)
                                        .Include(p => p.Pagamentos).ThenInclude(pg => pg.TipoPagamento)
                                        .Include(p => p.StatusPedido);

            var total = await query.CountAsync();
            var pedidos = await query.OrderBy(p => p.Id).Skip(skip).Take(pageSize).ToListAsync();


            return (pedidos, total);
        }

        public async Task<Pedido> GetByIdAsync(int id)
        {
            return await _context.Pedidos.Include(p => p.Cliente).ThenInclude(p => p.Endereco)
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
