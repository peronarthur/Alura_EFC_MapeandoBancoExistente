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
    //Estou falando que a PessoaConfiguration é do tipo Genérico T e que esse tipo deve herdar pessoa
    public class PessoaConfiguration<T> : IEntityTypeConfiguration<T> where T : Pessoa
    {
        //precisa do virtual pra poder ser sobreescrito
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder
               .Property(x => x.PrimeiroNome)
               .HasColumnName("first_name")
               .HasColumnType("varchar(45)")
               .IsRequired();

            builder
                .Property(x => x.UltimoNome)
                .HasColumnName("last_name")
                .HasColumnType("varchar(45)")
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(50)");

            builder
                .Property(x => x.Ativo)
                .HasColumnName("active")
                .HasColumnType("bit")
                .IsRequired();
            
            builder
                .Property<DateTime>("last_update")
                .HasColumnType("datetime")
                .HasComputedColumnSql("getdate()");
        }
    }
}
