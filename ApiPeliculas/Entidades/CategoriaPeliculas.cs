namespace ApiPeliculas.Entidades
{
    public class CategoriaPeliculas
    {
        public int CategoriaId { get; set; }
        public int PeliculaId { get; set; }
        public bool Ganador { get; set; }
        public Categoria Categoria { get; set; }
        public Pelicula Pelicula { get; set; }
    }
}
