using Alura.Filmes.App.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Filmes.App.Extensions
{
    public static class ClassificacaoIndicativaExtensions
    {
        private static Dictionary<string, ClassificacaoIndicativa> mapaClassificacaoIndicativa = new
            Dictionary<string, ClassificacaoIndicativa>{
            {"G", ClassificacaoIndicativa.Livre    },
            {"PG", ClassificacaoIndicativa.MaioresQue10 },
            {"PG-13", ClassificacaoIndicativa.MaioresQue13 },
            {"R", ClassificacaoIndicativa.MaioresQue14 },
            {"NC-17", ClassificacaoIndicativa.MaioresQue18}};

        public static string ParaString(this ClassificacaoIndicativa valor)
        {
            return mapaClassificacaoIndicativa.FirstOrDefault(x => x.Value == valor).Key;
        }

        public static ClassificacaoIndicativa ParaValor(this string texto)
        {
            return mapaClassificacaoIndicativa.FirstOrDefault(x => x.Key == texto).Value;
        }
    }
}
