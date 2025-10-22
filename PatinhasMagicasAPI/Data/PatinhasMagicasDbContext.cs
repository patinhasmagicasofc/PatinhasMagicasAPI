using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Models;

namespace PatinhasMagicasAPI.Data
{
    public class PatinhasMagicasDbContext : DbContext
    {
        public PatinhasMagicasDbContext(DbContextOptions<PatinhasMagicasDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<TipoUsuario> TiposUsuario { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<StatusAgendamento> StatusAgendamento { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<TipoPagamento> TipoPagamento { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<TipoServico> TipoServico { get; set; }
        public DbSet<StatusPedido> StatusPedido { get; set; }
        public DbSet<StatusPagamento> StatusPagamento { get; set; }
        public DbSet<TamanhoAnimal> TamanhoAnimal { get; set; }
        public DbSet<Animal> Animais { get; set; }
        public DbSet<ServicoTamanho> ServicoTamanho { get; set; }
        public DbSet<AgendamentoServico> AgendamentoServico { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Default Values para datas
            // -------------------------------
            // Default Values para datas
            // -------------------------------
            modelBuilder.Entity<Agendamento>()
                .Property(a => a.DataCadastro)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Pedido>()
                .Property(p => p.DataPedido)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Pagamento>()
                .Property(p => p.DataPagamento)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            // -------------------------------
            // Default Values para Ativo
            // -------------------------------
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Ativo)
                .HasDefaultValue(true);

            modelBuilder.Entity<Servico>()
                .Property(s => s.Ativo)
                .HasDefaultValue(true);

            // -------------------------------
            // Decimal Precision
            // -------------------------------
            modelBuilder.Entity<Produto>().Property(p => p.Preco).HasPrecision(10, 2);
            modelBuilder.Entity<ItemPedido>().Property(i => i.PrecoUnitario).HasPrecision(10, 2);
            modelBuilder.Entity<Pagamento>().Property(p => p.Valor).HasPrecision(10, 2);
            modelBuilder.Entity<ServicoTamanho>().Property(s => s.Preco).HasPrecision(10, 2);
            modelBuilder.Entity<AgendamentoServico>().Property(s => s.Preco).HasPrecision(10, 2);

            // -------------------------------
            // Relacionamentos
            // -------------------------------

            // Animal
            modelBuilder.Entity<Animal>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Animais)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Animal>()
                .HasOne(a => a.Especie)
                .WithMany(e => e.Animais)
                .HasForeignKey(a => a.EspecieId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Animal>()
                .HasOne(a => a.TamanhoAnimal)
                .WithMany(t => t.Animais)
                .HasForeignKey(a => a.TamanhoAnimalId)
                .OnDelete(DeleteBehavior.Restrict);

            // Agendamento
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

            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.StatusAgendamento)
                .WithMany(s => s.Agendamentos)
                .HasForeignKey(a => a.StatusAgendamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Pedido
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.StatusPedido)
                .WithMany(s => s.Pedidos)
                .HasForeignKey(p => p.StatusPedidoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemPedido
            modelBuilder.Entity<ItemPedido>()
                .HasOne(i => i.Pedido)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemPedido>()
                .HasOne(i => i.Produto)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Pagamento
            modelBuilder.Entity<Pagamento>()
                .HasOne(p => p.Pedido)
                .WithMany(pe => pe.Pagamentos)
                .HasForeignKey(p => p.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pagamento>()
                .HasOne(p => p.TipoPagamento)
                .WithMany(tp => tp.Pagamentos)
                .HasForeignKey(p => p.TipoPagamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pagamento>()
                .HasOne(p => p.StatusPagamento)
                .WithMany(sp => sp.Pagamentos)
                .HasForeignKey(p => p.StatusPagamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Produto
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Especie)
                .WithMany()
                .HasForeignKey(p => p.EspecieId)
                .OnDelete(DeleteBehavior.Restrict);

            // Servico
            modelBuilder.Entity<Servico>()
                .HasOne(s => s.TipoServico)
                .WithMany(ts => ts.Servicos)
                .HasForeignKey(s => s.TipoServicoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ServicoTamanho
            modelBuilder.Entity<ServicoTamanho>()
                .HasOne(st => st.TamanhoAnimal)
                .WithMany(t => t.ServicoTamanhos)
                .HasForeignKey(st => st.TamanhoAnimalId)
                .OnDelete(DeleteBehavior.Restrict);

            // AgendamentoServico
            modelBuilder.Entity<AgendamentoServico>()
                .HasOne(asv => asv.Agendamento)
                .WithMany(a => a.AgendamentoServicos)
                .HasForeignKey(asv => asv.AgendamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AgendamentoServico>()
                .HasOne(asv => asv.Servico)
                .WithMany(s => s.AgendamentoServicos)
                .HasForeignKey(asv => asv.ServicoId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}