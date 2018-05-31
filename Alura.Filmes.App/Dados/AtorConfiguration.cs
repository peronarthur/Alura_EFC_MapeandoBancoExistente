using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Filmes.App.Dados
{
    public class AtorConfiguration : IEntityTypeConfiguration<Ator>
    {
        void IEntityTypeConfiguration<Ator>.Configure(EntityTypeBuilder<Ator> builder)
        {
            builder
                .ToTable("actor");

            builder
                .Property(x => x.Id)
                .HasColumnName("actor_id");

            builder
                .Property(x => x.PrimeiroNome)
                .IsRequired()
                .HasColumnType("varchar(45)")
                .HasColumnName("first_name");

            builder
                .Property(x => x.UltimoNome)
                .IsRequired()
                .HasColumnType("varchar(45)")
                .HasColumnName("last_name");

            builder
                .Property<DateTime>("last_update")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            //Criando um índice em uma coluna que não é chave estrageira
            //mudando o nome padrão do índice, que por conversão é ix_(nomeTabela)_(nomeColuna)
            builder
                .HasIndex(x => x.UltimoNome)
                .HasName("idx_actor_last_name");
        }
    }
}
