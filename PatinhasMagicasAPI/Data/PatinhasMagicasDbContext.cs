using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Data
{
    public class PatinhasMagicasDbContext : DbContext
    {
        public PatinhasMagicasDbContext(DbContextOptions<PatinhasMagicasDbContext> options) : base(options) {}

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<TipoUsuario> TiposUsuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<StatusAgendamento> StatusAgendamentos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<TipoPagamento> TipoPagamentos{ get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<TipoServico> TiposServico { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoServico>()
                .HasKey(t => t.TipoServicoId);

            modelBuilder.Entity<Servico>()
                .HasKey(s => s.IdServico);

            modelBuilder.Entity<Servico>()
                .HasOne(s => s.TipoServico)
                .WithMany(t => t.Servicos)
                .HasForeignKey(s => s.TipoServicoId);
        }

    }

}

