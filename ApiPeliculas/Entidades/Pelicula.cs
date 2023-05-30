using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }
        public int NumOscars { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string Poster { get; set; }
        public List<PeliculasActores> PeliculasActores { get; set; }
        public List<PeliculasGeneros> PeliculasGeneros { get; set; }
        public List<PeliculasSalasDeCine> PeliculasSalasDeCines { get; set; }
    }
}
