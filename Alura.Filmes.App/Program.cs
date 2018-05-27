﻿using Alura.Filmes.App.Dados;
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

                
                foreach (var filme in contexto.Filmes)
                {
                    Console.WriteLine(filme);
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