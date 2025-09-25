using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Data
{
    public class PatinhasMagicasDbContext : DbContext
    {
        public PatinhasMagicasDbContext(DbContextOptions<PatinhasMagicasDbContext> options) : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
    }
}
