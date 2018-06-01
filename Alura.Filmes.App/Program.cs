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
        ///Entity com herança
        ///utiliza a classe mais ancestral e mapeia nela todas as colunas da classe filha
        ///TPH - Table Per Hierarchy == entity core só consegue utilizar esse tipo
        ///Cria uma tabela por classe concreta 
        ///TPC - Table Per Concrete Type
        ///Cria uma tabela por tipo de tabela
        ///TPT - Table per Type
        ///Para usar herança, no EFCore, deve-se utilizar como no exemplo:
        ///remove a tabela pai (Pessoa) do mapeamento e só mapeia as filhas (Cliente, Funcionário)


        static void Main(string[] args)
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                Console.WriteLine("Clientes: ");
                foreach (var cliente in contexto.Clientes)
                {
                    Console.WriteLine(cliente);
                }

                Console.WriteLine("\nFuncionários");
                foreach (var funcionario in contexto.Funcionarios)
                {
                    Console.WriteLine(funcionario);
                }
            }
        }

        private static void ConvertendoEnumParaValor()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var livre = ClassificacaoIndicativa.MaioresQue13;

                Console.WriteLine(livre.ParaString());
                Console.WriteLine(livre.ParaString().ParaValor());
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