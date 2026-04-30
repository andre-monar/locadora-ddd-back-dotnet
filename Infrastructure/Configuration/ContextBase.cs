using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<Carro> Carro { get; set; }
        public DbSet<Alocacao> Alocacao { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<CategoriaCarro> CategoriaCarro { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(GetStringConectionConfig());
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Carro>()
                .HasIndex(c => c.Placa)
                .IsUnique();

            builder.Entity<CategoriaCarro>()
                .HasIndex(c => c.Nome)
                .IsUnique();

            builder.Entity<Cliente>()
                .HasIndex(c => c.CPF)
                .IsUnique();

            builder.Entity<Cliente>()
                .HasIndex(c => c.Email)
                .IsUnique();

            // integridade referencial
            builder.Entity<Carro>()
                .HasOne(c => c.Categoria)
                .WithMany(cat => cat.Carros)
                .HasForeignKey(c => c.IdCategoria)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Alocacao>()
                .HasOne(a => a.Carro)
                .WithMany(c => c.Alocacoes)
                .HasForeignKey(a => a.IdCarro)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Alocacao>()
                .HasOne(a => a.Cliente)
                .WithMany(c => c.Alocacoes)
                .HasForeignKey(a => a.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(builder);
        }

        private string GetStringConectionConfig()
        {
            return "Host=postgres;Port=5432;Database=locadora;Username=postgres;Password=1234";
        }
    }
}