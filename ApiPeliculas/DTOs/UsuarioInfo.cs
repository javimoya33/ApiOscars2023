using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.DTOs
{
    public class UsuarioInfo
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
