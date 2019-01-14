using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Superdigital.Backend.ContaCorrente.Domain;
using Superdigital.Backend.ContaCorrente.Domain.Entities;

namespace Superdigital.Backend.ContaCorrente.Data.Context.FluentAPIConfig
{
    public class ContaClienteConfig : IEntityTypeConfiguration<ContaCliente>
    {
        public void Configure(EntityTypeBuilder<ContaCliente> builder)
        {
            builder.Property(c => c.NomeCliente).HasMaxLength(100);
        }
    }
}