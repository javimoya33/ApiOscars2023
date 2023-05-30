namespace ApiPeliculas.Entidades
{
    public class PeliculasActores
    {
        public int ActorId { get; set; }
        public int PeliculaId { get; set; }
        public string Personaje { get; set; }
        public int Orden { get; set; }
        public Actor actor { get; set; }
        public Pelicula pelicula { get; set; }
    }
}
