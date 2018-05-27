using System;

namespace Alura.Filmes.App
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string AnoLancamento { get; set; }
        public Int16 Duracao { get; set; }
        public string ClassificacaoEtaria { get; set; }

        public override string ToString()
        {
            return $"({Id}) Filme {Titulo}({AnoLancamento})[{Duracao} min] - {Descricao}. Classificação etária: {ClassificacaoEtaria} ";
        }
    }
}
