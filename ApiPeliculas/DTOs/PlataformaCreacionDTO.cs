using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.DTOs
{
    public class PlataformaCreacionDTO
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
    }
}
