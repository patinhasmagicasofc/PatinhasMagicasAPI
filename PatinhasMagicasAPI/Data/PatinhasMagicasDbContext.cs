using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Data
{
    public class PatinhasMagicasDbContext : DbContext
    {
        public PatinhasMagicasDbContext(DbContextOptions<PatinhasMagicasDbContext> options) : base(options) { }

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
        public DbSet<TipoPagamento> TipoPagamentos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<TipoServico> TiposServico { get; set; }
        public DbSet<StatusPedido> StatusPedidos { get; set; }
        public DbSet<StatusPagamento> StatusPagamentos { get; set; }
        public DbSet<TamanhoAnimal> TamanhoAnimals { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<ServicoTamanho> ServicoTamanhos { get; set; }
        public DbSet<AgendamentoServico> AgendamentoServicos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Animal)
                .WithMany(an => an.Agendamentos)
                .HasForeignKey(a => a.AnimalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Pedido)
                .WithMany(p => p.Agendamentos)
                .HasForeignKey(a => a.PedidoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ItemPedido>()
                .Property(i => i.PrecoUnitario)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Pagamento>()
                .Property(p => p.Valor)
                .HasPrecision(10, 2);

            modelBuilder.Entity<AgendamentoServico>()
                .Property(s => s.Preco)
                .HasPrecision(10, 2);
        }

    }
}