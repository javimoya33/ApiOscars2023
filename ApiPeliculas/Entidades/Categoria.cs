namespace ApiPeliculas.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<CategoriaPeliculas> CategoriaPeliculas { get; set; }
        public List<CategoriasActores> CategoriaActores { get; set; }
    }
}
