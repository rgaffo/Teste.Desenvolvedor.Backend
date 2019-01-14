using Microsoft.EntityFrameworkCore;
using Superdigital.Backend.ContaCorrente.Data.Context.FluentAPIConfig;
using Superdigital.Backend.ContaCorrente.Domain.Entities;

namespace Superdigital.Backend.ContaCorrente.Data.Context
{
    public class ContaCorrenteContext : DbContext
    {
        public DbSet<ContaCliente> ContasClientes { get; set; }
        public DbSet<Lancamento> Lancamentos { get; set; }

        public ContaCorrenteContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContaClienteConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}