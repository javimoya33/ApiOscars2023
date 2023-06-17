using ApiPeliculas.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.DTOs
{
    public class CategoriaActorCreacionDTO
    {
        public string Ganador { get; set; }
        public int CategoriaId { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> ActorIds { get; set; }

    }
}
