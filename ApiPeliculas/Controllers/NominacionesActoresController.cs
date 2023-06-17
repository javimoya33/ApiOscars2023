using ApiPeliculas.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Controllers
{
    [ApiController]
    [Route("/api/actores/nominados")]
    public class NominacionesActoresController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public NominacionesActoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet ("{CategoriaId:int}")]
        public async Task<ActionResult<List<ActorNominadoDTO>>> Get(int categoriaId)
        {
            var actoresNominados = await context.Actores
                .Include(x => x.CategoriasActores).ThenInclude(x => x.Actor)
                .Where(x => x.CategoriasActores.Any(x => x.CategoriaId == categoriaId))
                .ToListAsync();

            if (actoresNominados == null)
            {
                return NotFound();
            }

            var dtos = new List<ActorNominadoDTO>();

            foreach (var actorNominado in actoresNominados)
            {
                var actorNominadoDTO = new ActorNominadoDTO
                {
                    Nombre = actorNominado.Nombre,
                    FechaNacimiento = actorNominado.FechaNacimiento,
                    Foto = actorNominado.Foto
                };

                var categoriaActor = actorNominado.CategoriasActores.FirstOrDefault(x => x.CategoriaId == categoriaId);

                if (categoriaActor != null)
                {
                    actorNominadoDTO.Ganador = categoriaActor.Ganador;
                }

                dtos.Add(actorNominadoDTO);
            }

            return dtos;
        }

        [HttpGet("oscars")]
        public async Task<ActionResult<List<ActorDTO>>> actoresConOscars()
        {
            var actoresConOscars = await context.Actores
                .Join(context.CategoriasActores, a => a.Id, ca => ca.ActorId, (a, ca) => new { Actor = a, CategoriaActores = ca })
                .Where(ca => ca.CategoriaActores.Ganador)
                .Select(x => x.Actor)
                .ToListAsync();

            if (actoresConOscars == null)
            {
                return NotFound();
            }

            var dtos = mapper.Map<List<ActorDTO>>(actoresConOscars);
            return dtos;
        }
    }
}
