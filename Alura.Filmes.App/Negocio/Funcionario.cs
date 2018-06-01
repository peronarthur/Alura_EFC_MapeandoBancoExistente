using Alura.Filmes.App.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Filmes.App.Negocio
{
    public class Funcionario : Pessoa
    {
        public string  Login { get; set; }
        public string Senha { get; set; }
    }
}
