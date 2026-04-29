using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<Cliente>
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
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cliente>().ToTable("AspNetUsers").HasKey(t => t.Id);

            // Índice único para placa
            builder.Entity<Carro>()
                .HasIndex(c => c.Placa)
                .IsUnique();

            // Nome de categoria de carro tb deve ser único
            builder.Entity<CategoriaCarro>()
                .HasIndex(c => c.Nome)
                .IsUnique();

            // CPF e emails de cliente
            builder.Entity<Cliente>()
                .HasIndex(c => c.CPF)
                .IsUnique();

            builder.Entity<Cliente>()
                .HasIndex(c => c.Email)
                .IsUnique();

            // Índice único nome CategoriaCarro
            builder.Entity<CategoriaCarro>()
                .HasIndex(c => c.Nome)
                .IsUnique();

            base.OnModelCreating(builder);
        }

        private string GetStringConectionConfig()
        {
            string strCon = "Data Source=DESKTOP-HVNTI80\\DESENVOLVIMENTO;Initial Catalog=DDD_ECOMMERCE;Integrated Security=False;User ID=sa;Password=1234;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            return strCon;
        }
    }
}