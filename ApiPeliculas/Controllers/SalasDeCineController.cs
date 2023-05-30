using ApiPeliculas.DTOs;
using ApiPeliculas.Entidades;
using ApiPeliculas.Migrations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace ApiPeliculas.Controllers
{
    [Route("api/salasdecine")]
    [ApiController]
    public class SalasDeCineController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly GeometryFactory geometryFactory;

        public SalasDeCineController(ApplicationDbContext context, IMapper mapper, GeometryFactory geometryFactory)
        {
            this.context = context;
            this.mapper = mapper;
            this.geometryFactory = geometryFactory;
        }

        [HttpGet]
        public async Task<ActionResult<List<SalasDeCineDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.SalasDeCine.AsQueryable();

            return mapper.Map<List<SalasDeCineDTO>>(queryable);
        }

        [HttpGet("{id:int}", Name = "obtenerSalaDeCine")]
        public async Task<ActionResult<SalasDeCineDTO>> Get(int id)
        {
            var entidad = await context.SalasDeCine.FirstOrDefaultAsync(x => x.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<SalasDeCineDTO>(entidad);

            return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SalasDeCineCreacionDTO salasDeCineCreacionDTO)
        {
            var salaDeCine = mapper.Map<SalaDeCine>(salasDeCineCreacionDTO);

            context.Add(salaDeCine);
            await context.SaveChangesAsync();

            var dto = mapper.Map<SalasDeCineDTO>(salaDeCine);
            return new CreatedAtRouteResult("obtenerSalaDeCine", new { id = salaDeCine.Id }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromBody] SalasDeCineCreacionDTO salasDeCineCreacionDTO, int id)
        {
            var salasDeCineDB = await context.SalasDeCine.FirstOrDefaultAsync(x => x.Id == id);

            if (salasDeCineDB == null)
            {
                return NotFound();
            }

            salasDeCineDB = mapper.Map(salasDeCineCreacionDTO, salasDeCineDB);

            await context.SaveChangesAsync();

            var dto = mapper.Map<SalasDeCineDTO>(salasDeCineDB);
            return new CreatedAtRouteResult("obtenerSalaDeCine", new { id = salasDeCineDB.Id }, dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.SalasDeCine.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new SalaDeCine { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("Cercanos")]
        public async Task<ActionResult<List<SalaDeCineCercanoDTO>>> Cercanos([FromQuery] SalaDeCineCercanoFiltroDTO filtro)
        {
            var ubicacionUsuario = geometryFactory.CreatePoint(new Coordinate(filtro.Longitud, filtro.Latitud));

            var salasDeCine = await context.SalasDeCine
                .OrderBy(x => x.Ubicacion.Distance(ubicacionUsuario))
                .Where(x => x.Ubicacion.IsWithinDistance(ubicacionUsuario, filtro.DistanciaEnKms * 1000))
                .Select(x => new SalaDeCineCercanoDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Latitud = x.Ubicacion.Y,
                    Longitud = x.Ubicacion.X,
                    DistanciaEnMetros = Math.Round(x.Ubicacion.Distance(ubicacionUsuario))
                }).ToListAsync();

            return salasDeCine;
        }
    }
}
