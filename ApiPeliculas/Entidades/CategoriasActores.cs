namespace ApiPeliculas.Entidades
{
    public class CategoriasActores
    {
        public int CategoriaId { get; set; }
        public int ActorId { get; set; }
        public bool Ganador { get; set; }
        public Categoria Categoria { get; set; }
        public Actor Actor { get; set; }
    }
}
