using System;
using System.Collections.Generic;

namespace Alura.Filmes.App.Negocio
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string AnoLancamento { get; set; }
        public Int16 Duracao { get; set; }
        public string ClassificacaoEtaria { get; set; }

        public IList<FilmeAtor> Atores { get; set; }
        public IList<FilmeCategoria> Categorias { get; set; }

        public Filme()
        {
            Atores = new List<FilmeAtor>();
            Categorias = new List<FilmeCategoria>();
        }

        public override string ToString()
        {
            return $"({Id}) Filme {Titulo}({AnoLancamento})[{Duracao} min] - {Descricao}. Classificação etária: {ClassificacaoEtaria} ";
        }
    }
}
