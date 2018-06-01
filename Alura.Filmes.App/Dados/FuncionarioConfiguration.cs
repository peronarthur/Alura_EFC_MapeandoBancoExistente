using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alura.Filmes.App.Dados
{
    public class FuncionarioConfiguration : PessoaConfiguration<Funcionario>
    {
        public override void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            base.Configure(builder);

            builder
                .ToTable("staff");

            builder
                .Property(x => x.Id)
                .HasColumnName("staff_id");

            builder
                .Property(x => x.Login)
                .HasColumnName("username")
                .HasColumnType("varchar(16)")
                .IsRequired();

            builder
                .Property(x => x.Senha)
                .HasColumnName("password")
                .HasColumnType("varchar(40)");




        }
    }
}