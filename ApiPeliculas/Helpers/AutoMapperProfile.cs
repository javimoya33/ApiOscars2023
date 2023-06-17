using ApiPeliculas.DTOs;
using ApiPeliculas.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;

namespace ApiPeliculas.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile(GeometryFactory geometryFactory)
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<Review, ReviewDTO>()
                .ForMember(x => x.NombreUsuario, x => x.MapFrom(y => y.Usuario.UserName));

            CreateMap<ReviewDTO, Review>();
            CreateMap<ReviewCreacionDTO, Review>();

            CreateMap<IdentityUser, UsuarioDTO>();

            CreateMap<SalaDeCine, SalasDeCineDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.Ubicacion.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.Ubicacion.X));

            CreateMap<SalasDeCineDTO, SalaDeCine>()
                .ForMember(x => x.Ubicacion, x => x.MapFrom(y =>
                    geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<SalasDeCineCreacionDTO, SalaDeCine>()
                .ForMember(x => x.Ubicacion, x => x.MapFrom(y =>
                    geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>()
                .ForMember(x => x.Foto, options => options.Ignore());

            CreateMap<Pelicula, PeliculaDTO>().ReverseMap();
            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.PeliculasGeneros, options => options.MapFrom(MapPeliculasGeneros))
                .ForMember(x => x.PeliculasActores, options => options.MapFrom(MapPeliculasActores));

            CreateMap<Pelicula, PeliculaDetallesDTO>()
                .ForMember(x => x.Generos, options => options.MapFrom(MapPeliculasGeneros))
                .ForMember(x => x.Actores, options => options.MapFrom(MapPeliculasActores));

            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<CategoriaCreacionDTO, Categoria>()
                .ForMember(x => x.CategoriaPeliculas, options => options.MapFrom(MapCategoriaPeliculas))
                .ForMember(x => x.CategoriaActores, options => options.MapFrom(MapCategoriasActores));
            CreateMap<CategoriaGanadorDTO, Categoria>().ReverseMap();

            CreateMap<CategoriaPeliculaDTO, CategoriaPeliculas>().ReverseMap();
            CreateMap<CategoriaPeliculaCreacionDTO, CategoriaPeliculas>();
            CreateMap<Pelicula, PeliculaNominadasDTO>().ReverseMap();

            CreateMap<CategoriaActorDTO, CategoriasActores>().ReverseMap();
            CreateMap<CategoriaActorCreacionDTO, CategoriasActores>();
            CreateMap<Actor, ActorNominadoDTO>().ReverseMap();
        }

        private List<CategoriasActores> MapCategoriasActores(CategoriaCreacionDTO categoriaCreacionDTO, Categoria categoria)
        {
            var resultado = new List<CategoriasActores>();

            if (categoriaCreacionDTO.ActorIds == null)
            {
                return resultado;
            }

            foreach (var id in categoriaCreacionDTO.ActorIds)
            {
                resultado.Add(new CategoriasActores()
                {
                    ActorId = id
                }); ;
            }

            return resultado;
        }

        private List<CategoriaPeliculas> MapCategoriaPeliculas(CategoriaCreacionDTO CategoriaCreacionDTO,
            Categoria categoria)
        {
            var resultado = new List<CategoriaPeliculas>();

            if (CategoriaCreacionDTO.PeliculaIds == null)
            {
                return resultado;
            }

            foreach (var id in CategoriaCreacionDTO.PeliculaIds)
            {
                resultado.Add(new CategoriaPeliculas()
                {
                    PeliculaId = id
                });
            }

            return resultado;
        }

        private List<ActorPeliculaDetalleDTO> MapPeliculasActores(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDTO)
        {
            var resultado = new List<ActorPeliculaDetalleDTO>();

            if (pelicula.PeliculasActores == null)
            {
                return resultado;
            }

            foreach (var actorPelicula in pelicula.PeliculasActores)
            {
                resultado.Add(new ActorPeliculaDetalleDTO
                {
                    ActorId = actorPelicula.ActorId,
                    Personaje = actorPelicula.Personaje,
                    NombreActor = actorPelicula.Actor.Nombre
                });
            }

            return resultado;
        }

        private List<GeneroDTO> MapPeliculasGeneros(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDTO)
        {
            var resultado = new List<GeneroDTO>();

            if (pelicula.PeliculasGeneros != null)
            {
                return resultado;
            }

            foreach (var generoPelicula in pelicula.PeliculasGeneros)
            {
                resultado.Add(new GeneroDTO()
                {
                    Id = generoPelicula.GeneroId, 
                    Nombre = generoPelicula.Genero.Nombre
                });
            }

            return resultado;
        }

        private List<PeliculasGeneros> MapPeliculasGeneros(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasGeneros>();

            if (peliculaCreacionDTO.GenerosIds == null)
            {
                return resultado;
            }

            foreach (var id in peliculaCreacionDTO.GenerosIds)
            {
                resultado.Add(new PeliculasGeneros() { GeneroId = id });
            }

            return resultado;
        }

        private List<PeliculasActores> MapPeliculasActores(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasActores>();

            if (peliculaCreacionDTO.Actores == null)
            {
                return resultado;
            }

            foreach (var actor in peliculaCreacionDTO.Actores)
            {
                resultado.Add(new PeliculasActores() 
                { 
                    ActorId = actor.ActorId, 
                    Personaje = actor.Personaje 
                });
            }

            return resultado;
        }
    }
}
