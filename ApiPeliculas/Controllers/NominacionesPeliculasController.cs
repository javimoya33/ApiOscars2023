using ApiPeliculas.DTOs;
using ApiPeliculas.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace ApiPeliculas.Controllers
{
    [ApiController]
    [Route("api/peliculas/nominadas")]
    public class NominacionesPeliculasController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public NominacionesPeliculasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PeliculasIndexNominadasDTO>>> Get()
        {
            var peliculasNominadas = context.Peliculas
                .Join(context.CategoriaPeliculas, p => p.Id, cp => cp.PeliculaId, (p, cp) => new { Pelicula = p, CategoriaPelicula = cp })
                .GroupBy(x => new { x.Pelicula.Id, x.Pelicula.Titulo })
                .Select(g => new
                {
                    Titulo = g.Key.Titulo,
                    Nominaciones = g.Count()
                })
                .OrderByDescending(x => x.Nominaciones);

            if (peliculasNominadas == null)
            {
                return BadRequest();
            }

            var dto = new List<PeliculasIndexNominadasDTO>();

            foreach (var peliculaNominada in peliculasNominadas)
            {
                var peliculaNominadaDTO = new PeliculasIndexNominadasDTO
                {
                    Titulo = peliculaNominada.Titulo,
                    Nominaciones = peliculaNominada.Nominaciones
                };

                dto.Add(peliculaNominadaDTO);
            }

            return dto;
        }

        [HttpGet("oscars")]
        public async Task<ActionResult<List<peliculasIndexGanadorasDTO>>> peliculasGanadoras()
        {
            var peliculasNominadas = context.Peliculas
                .Join(context.CategoriaPeliculas, p => p.Id, cp => cp.PeliculaId, (p, cp) => new { Pelicula = p, CategoriaPelicula = cp })
                .Where(cp => cp.CategoriaPelicula.Ganador)
                .GroupBy(x => new { x.Pelicula.Id, x.Pelicula.Titulo })
                .Select(g => new
                {
                    Titulo = g.Key.Titulo,
                    Oscars = g.Count()
                })
                .OrderByDescending(x => x.Oscars);

            if (peliculasNominadas == null)
            {
                return BadRequest();
            }

            var dto = new List<peliculasIndexGanadorasDTO>();

            foreach (var peliculaNominada in peliculasNominadas)
            {
                var peliculaNominadaDTO = new peliculasIndexGanadorasDTO
                {
                    Titulo = peliculaNominada.Titulo,
                    Oscars = peliculaNominada.Oscars
                };

                dto.Add(peliculaNominadaDTO);
            }

            return dto;
        }

        [HttpGet("{CategoriaId:int}")]
        public async Task<ActionResult<List<PeliculaNominadasDTO>>> Get(int categoriaId)
        {
            var peliculasNominadas = await context.Peliculas
                .Include(x => x.CategoriaPeliculas).ThenInclude(x => x.Categoria)
                .Where(x => x.CategoriaPeliculas.Any(cp => cp.CategoriaId == categoriaId))
                .OrderBy(x => x.Titulo)
                .ToListAsync();

            if (peliculasNominadas == null)
            {
                return NotFound();
            }

            var dtos = new List<PeliculaNominadasDTO>();

            foreach (var peliculaNominada in peliculasNominadas)
            {
                var peliculaNominadaDTO = new PeliculaNominadasDTO
                {
                    Titulo = peliculaNominada.Titulo,
                    NumOscars = peliculaNominada.NumOscars,
                    FechaEstreno = peliculaNominada.FechaEstreno,
                    Poster = peliculaNominada.Poster
                };

                var categoriaPelicula = peliculaNominada.CategoriaPeliculas.FirstOrDefault(x => x.CategoriaId == categoriaId);

                if (categoriaPelicula != null)
                {
                    peliculaNominadaDTO.Ganador = categoriaPelicula.Ganador;
                }

                dtos.Add(peliculaNominadaDTO);
            }

            //var dtos = mapper.Map<List<PeliculaNominadasDTO>>(peliculasNominadas);

            return dtos;
        }
    }
}
