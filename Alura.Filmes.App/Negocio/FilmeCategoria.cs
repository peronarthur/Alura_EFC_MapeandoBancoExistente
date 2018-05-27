using Alura.Filmes.App.Negocio;

namespace Alura.Filmes.App.Negocio
{
    public class FilmeCategoria
    {
        public int FilmeId { get; set; }
        public byte CategoriaId { get; set; }

        public Filme Filme { get; set; }
        public Categoria Categoria { get; set; }
    }
}