using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.DTOs
{
    public class SalasDeCineCreacionDTO
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        [Range (-90, 90)]
        public double Latitud { get; set; }
        [Range (-90, 90)]
        public double Longitud { get; set; }
    }
}
