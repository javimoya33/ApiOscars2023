using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.DTOs
{
    public class PeliculaDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }
        public int NumOscars { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string Poster { get; set; }
    }
}
