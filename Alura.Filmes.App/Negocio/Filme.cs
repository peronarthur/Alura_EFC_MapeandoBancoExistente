using Alura.Filmes.App.Extensions;
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

        //Criada essa propriedade pra resolver o problema do banco legado que utiliza string nos valores
        //e na aplicação utilizar um enum com os tipos
        public string TextoClassificacao { get; private set; }
        public ClassificacaoIndicativa ClassificacaoEtaria
        {
            get { return TextoClassificacao.ParaValor(); }
            set { TextoClassificacao = value.ParaString(); }
        }

        public IList<FilmeAtor> Atores { get; set; }
        public IList<FilmeCategoria> Categorias { get; set; }

        public Idioma IdiomaFalado { get; set; }
        public Idioma IdiomaOriginal { get; set; }

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
