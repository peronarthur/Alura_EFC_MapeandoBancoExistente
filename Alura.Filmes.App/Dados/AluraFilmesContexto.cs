using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Filmes.App.Dados
{
    public class AluraFilmesContexto : DbContext
    {
        public DbSet<Ator> Atores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-GK5FL2O\\SQLEXPRESS;Database=AluraFilmes;user id=sa;pwd=minduin;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Ator>()
                .ToTable("actor");

            modelBuilder
                .Entity<Ator>()
                .Property(x => x.Id)
                .HasColumnName("actor_id");

            modelBuilder
                .Entity<Ator>()
                .Property(x => x.PrimeiroNome)
                .IsRequired()
                .HasColumnType("varchar(45)")
                .HasColumnName("first_name");

            modelBuilder
                .Entity<Ator>()
                .Property(x => x.UltimoNome)
                .IsRequired()
                .HasColumnType("varchar(45)")                
                .HasColumnName("last_name");
            modelBuilder
                .Entity<Ator>()
                .Property<DateTime>("last_update")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()")
                .IsRequired();


            base.OnModelCreating(modelBuilder);
        }
    }
}
