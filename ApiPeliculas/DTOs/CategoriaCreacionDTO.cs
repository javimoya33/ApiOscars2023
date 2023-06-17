using ApiPeliculas.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.DTOs
{
    public class CategoriaCreacionDTO
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> PeliculaIds { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> ActorIds { get; set; }
    }
}
