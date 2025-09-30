using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PatinhasMagicasAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Servico> Servicos { get; set; }
        public DbSet<TipoServico> TiposServico { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoServico>()
                .HasKey(t => t.IdTipoServico);

            modelBuilder.Entity<Servico>()
                .HasKey(s => s.IdServico);

            modelBuilder.Entity<Servico>()
                .HasOne(s => s.TipoServico)
                .WithMany(t => t.Servicos)
                .HasForeignKey(s => s.IdTipoServico);
        }
    }
}
