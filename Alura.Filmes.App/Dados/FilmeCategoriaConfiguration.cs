using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Alura.Filmes.App.Dados
{
    internal class FilmeCategoriaConfiguration : IEntityTypeConfiguration<FilmeCategoria>
    {
        public void Configure(EntityTypeBuilder<FilmeCategoria> builder)
        {
            builder
                .ToTable("film_category");

            builder
                .Property(x => x.FilmeId)
                .HasColumnName("film_id");

            builder
                .Property(x => x.CategoriaId)
                .HasColumnType("byte")
                .HasColumnName("category_id");

            builder
                .HasKey(x => new { x.CategoriaId, x.FilmeId });

            builder
                .Property<DateTime>("last_update")
                .HasColumnType("datetime")
                .HasComputedColumnSql("getdate()");
        }
    }
}