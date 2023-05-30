using ApiPeliculas.Helpers;
using ApiPeliculas.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.DTOs
{
    public class PeliculaCreacionDTO
    {
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }
        public int NumOscars { get; set; }
        public DateTime FechaEstreno { get; set; }
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 4)]
        [TipoArchivoValidacion(GrupoTipoArchivo.Imagen)]
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenerosIds { get; set; }

        [ModelBinder(BinderType =typeof(TypeBinder<List<ActorPeliculasCreacionDTO>>))]
        public List<ActorPeliculasCreacionDTO> Actores { get; set; }
    }
}
