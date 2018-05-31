using Alura.Filmes.App.Dados;
using Alura.Filmes.App.Extensions;
using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Alura.Filmes.App
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var idiomas = contexto.Idiomas
                    .Include(i => i.FilmesFalados);

                foreach (var idioma in idiomas)
                {
                    Console.WriteLine(idioma);
                    foreach (var filme in idioma.FilmesFalados)
                    {
                        Console.WriteLine(filme);
                    }
                }
            }
        }

        private static void BuscaElencoDosFIlmes()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var filme = contexto
                    .Filmes
                    .Include(f => f.Atores)
                    .ThenInclude(x => x.Ator)
                    .FirstOrDefault(x => x.Id == 2);

                Console.WriteLine(filme);
                Console.WriteLine("Elenco:");

                //foreach (var item in contexto.Elenco)
                //{
                //    //Pega a entidade
                //    var entidade = contexto.Entry(item);
                //    //Pega a shadow property
                //    var filmId = entidade.Property("film_id").CurrentValue;
                //    //Pega a shadow property
                //    var actorId = entidade.Property("actor_id").CurrentValue;

                //    Console.WriteLine($"FilmId: {filmId} - ActorId: {actorId}");
                //}

                foreach (var ator in filme.Atores)
                {
                    Console.WriteLine($"Ator: {ator.Ator.PrimeiroNome} {ator.Ator.UltimoNome}");
                }

                var categoria = contexto
                    .Filmes
                    .Include(x => x.Categorias)
                    .ThenInclude(y => y.Categoria)
                    .FirstOrDefault(x => x.Id == filme.Id);

                Console.WriteLine("Categoria");
                foreach (var c in categoria.Categorias)
                {
                    Console.WriteLine($"Categoria: {c.Categoria.Id} - {c.Categoria.Nome}");
                }


            }
        }

        private static void ListaUltimo10AtoresCadastrados()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                //Modificando a shadowProperty
                //var ator = new Ator();
                //ator.PrimeiroNome = "Tom";
                //ator.UltimoNome = "Hanks";
                //contexto.Entry(ator).Property("last_update").CurrentValue = DateTime.Now;
                //contexto.Add(ator);

                ///Utilizando a shadow property em query
                ///O EF está dentro do using Microsoft.EntityFrameworkCore;
                var atores = contexto.Atores
                    .OrderByDescending(x => EF.Property<DateTime>(x, "last_update"))
                    .Take(10);

                foreach (var item in atores)
                {
                    Console.WriteLine(item + " - " + contexto.Entry(item).Property("last_update").CurrentValue);
                }

                //contexto.SaveChanges();

            }
        }
    }
}