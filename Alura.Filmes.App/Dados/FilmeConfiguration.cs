using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Alura.Filmes.App.Dados
{
    public class FilmeConfiguration : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {

            builder
                .ToTable("film");

            builder
                .Property(x => x.Id)
                .HasColumnName("film_id");

            builder
                .Property(x => x.Titulo)
                .HasColumnName("title")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder
                .Property(x => x.Descricao)
                .HasColumnName("description")
                .HasColumnType("text");

            builder
                .Property(x => x.AnoLancamento)
                .HasColumnName("release_year")
                .HasColumnType("varchar(4)");

            builder
                .Property(x => x.Duracao)
                .HasColumnName("length")
                .HasColumnType("smallint");

            builder
                .Property(x => x.ClassificacaoEtaria)
                .HasColumnName("rating")
                .HasColumnType("varchar(10)");

            builder
                .Property<DateTime>("last_update")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            builder
                .Property<byte>("language_id")
                .HasColumnType("tinyint");

            //definindo uma chave estrangueira que aceita nulo
            builder
                .Property<byte?>("original_language_id")
                .HasColumnType("tinyint");

            builder
                .HasOne(x => x.IdiomaFalado)
                .WithMany(y => y.FilmesFalados)
                .HasForeignKey("language_id");

            builder
                .HasOne(x => x.IdiomaOriginal)
                .WithMany(y => y.FilmesOriginais)
                .HasForeignKey("original_language_id");
        }
    }
}
