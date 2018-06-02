using Alura.Filmes.App.Dados;
using Alura.Filmes.App.Extensions;
using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
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

                var categoria = "Action"; //36

                var paramCategoria = new SqlParameter("catagoria", categoria);
                var paramTotal = new SqlParameter()
                {
                    ParameterName = "@total",
                    Size = 4,
                    Direction = System.Data.ParameterDirection.Output
                };

                contexto.Database
                    .ExecuteSqlCommand("execute total_actors_from_given_category @catagoria, @total OUT", paramCategoria, paramTotal);

                Console.WriteLine($"O total de atores na categoria {categoria} é de {paramTotal.Value}");
            }
        }

        private static void UtilizandoSQLManualParaSubustituirOEntity()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                //Usando SQL injetado na mão
                //Ele tem uma limitação, que dessa forma só pode voltar dados de uma coluna e deve conter todas as colunas
                //da entidade
                //var sql = @"    
                //            select a.*
                //            from actor a
                //            join (

                //             select top 5 a.actor_id, count(a.actor_id) total
                //             from actor a 
                //             join film_actor fa on a.actor_id = fa.actor_id
                //             group by a.actor_id, a.first_name, a.last_name
                //             order by total desc
                //             ) filmes on filmes.actor_id = a.actor_id";

                //var atoresMaisAtuantes = contexto
                //    .Atores
                //    .FromSql(sql)
                //    .Include(x=> x.Filmografia)
                //    .OrderByDescending(x=> x.Filmografia.Count);

                //Utilizando a view top5_most_starred_actors
                var sql = @"    
                            select a.*
                            from actor a
                            join top5_most_starred_actors filmes on filmes.actor_id = a.actor_id";

                var atoresMaisAtuantes = contexto
                    .Atores
                    .FromSql(sql)
                    .Include(x => x.Filmografia)
                    .OrderByDescending(x => x.Filmografia.Count);

                //Usando EF para fazer a query
                //var atoresMaisAtuantes = contexto
                //    .Atores
                //    .Include(x=> x.Filmografia)
                //    .OrderByDescending(x=> x.Filmografia.Count)
                //    .Take(5);

                foreach (var ator in atoresMaisAtuantes)
                {
                    Console.WriteLine($"O ator {ator.PrimeiroNome} {ator.UltimoNome} atuou em {ator.Filmografia.Count} filmes");
                }

            }
        }

        private static void MostrandoClientesEFuncionarios()
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