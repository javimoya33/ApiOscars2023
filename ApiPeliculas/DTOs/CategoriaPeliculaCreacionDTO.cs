using ApiPeliculas.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.DTOs
{
    public class CategoriaPeliculaCreacionDTO
    {
        public bool Ganador { get; set; }
        public int CategoriaId { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> PeliculaIds { get; set; }
    }
}
