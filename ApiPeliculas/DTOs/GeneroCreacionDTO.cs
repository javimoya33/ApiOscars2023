using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.DTOs
{
    public class GeneroCreacionDTO
    {
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}
