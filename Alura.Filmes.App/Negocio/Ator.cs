using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alura.Filmes.App.Negocio
{
    //Movendo essa especificação do nome da tabela para o ModelBuiler
    //[Table("actor")]
    public class Ator
    {
        //Movendo essa especificação da coluna parao ModelBuilder
        //[Column("actor_id")]
        public int Id { get; set; }

        //Movendo essa especificação da coluna parao ModelBuilder
        //[Required]
        //[Column("first_name", TypeName = "varchar(45)")]
        public string PrimeiroNome { get; set; }

        //Movendo essa especificação da coluna parao ModelBuilder
        //[Required]
        //[Column("last_name", TypeName = "varchar(45)")]
        public string UltimoNome { get; set; }

        public IList<FilmeAtor> Filmografia { get; set; }

        public Ator()
        {
            Filmografia = new List<FilmeAtor>();
        }
        public override string ToString()
        {
            return $"Ator ({Id}): {PrimeiroNome} {UltimoNome}";
        }
    }
}