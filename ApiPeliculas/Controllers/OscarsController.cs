using ApiPeliculas.DTOs;
using ApiPeliculas.Entidades;
using ApiPeliculas.Helpers;
using ApiPeliculas.Migrations;
using ApiPeliculas.Servicios;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Controllers
{
    public interface IRepositorioOscars
    {
        Task<IActionResult> Peliculas();
    }

    [Route("Api/oscars")]
    public class OscarsController: Controller, IRepositorioOscars
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public OscarsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet ("portada")]
        public async Task<IActionResult> Peliculas()
        {
            // Mostrar las 5 películas con más Oscars en 2023 y las 5 películas que se estrenaron recientemente
            var top = 5;
            var minOscars = 2;

            var peliculasMasOscars = await context.Peliculas
                .Where(x => x.NumOscars >= minOscars)
                .OrderByDescending(x => x.NumOscars)
                .Take(top)
                .ToListAsync();

            var ultimosEstrenos = await context.Peliculas
                .OrderByDescending(x => x.FechaEstreno)
                .Take(top)
                .ToListAsync();

            var resultado = new PeliculasIndexDTO();
            resultado.peliculasMasOscars = mapper.Map<List<PeliculaDTO>>(peliculasMasOscars);
            resultado.ultimosEstrenos = mapper.Map<List<PeliculaDTO>>(ultimosEstrenos);

            return View(resultado);
        }

        [HttpGet("nominaciones")]
        public async Task<IActionResult> Nominaciones()
        {
            // Mostrar las películas nominadas
            var peliculasNominadas = context.Peliculas
                .Join(context.CategoriaPeliculas, p => p.Id, cp => cp.PeliculaId, (p, cp) => new { Pelicula = p, CategoriaPelicula = cp })
                .GroupBy(x => new { x.Pelicula.Id, x.Pelicula.Titulo, x.Pelicula.Poster })
                .Select(g => new
                {
                    Titulo = g.Key.Titulo,
                    Nominaciones = g.Count(),
                    Poster = g.Key.Poster
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
                    Nominaciones = peliculaNominada.Nominaciones,
                    Poster = peliculaNominada.Poster
                };

                dto.Add(peliculaNominadaDTO);
            }

            return View(dto);
        }

        [HttpGet("pelicula/{id:int}")]
        public async Task<IActionResult> Pelicula(int id)
        {
            var pelicula = await context.Peliculas
                .Include(x => x.PeliculasActores).ThenInclude(x => x.Actor)
                .Include(x => x.PeliculasGeneros).ThenInclude(x => x.Genero)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (pelicula == null)
            {
                return NotFound();
            }

            pelicula.PeliculasActores = pelicula.PeliculasActores.OrderBy(x => x.Orden).ToList();

            return View(pelicula);
        }
    }
}
