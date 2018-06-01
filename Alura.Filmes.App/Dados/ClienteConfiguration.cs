using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Alura.Filmes.App.Dados
{
    public class ClienteConfiguration : PessoaConfiguration<Cliente>
    {
        public override void Configure(EntityTypeBuilder<Cliente> builder)
        {
            base.Configure(builder);

            builder
                .ToTable("customer");

            builder
                .Property(x => x.Id)
                .HasColumnName("customer_id")
                .HasColumnType("tinyint");
            
            builder
                .Property<DateTime>("create_date")
                .HasColumnType("datetime")
                .HasComputedColumnSql("getdate()");

        }
    }
}