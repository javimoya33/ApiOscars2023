using ApiPeliculas.DTOs;
using ApiPeliculas.Entidades;
using ApiPeliculas.Migrations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CategoriasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet (Name = "obtenerCategorias")]
        public async Task<ActionResult<List<CategoriaDTO>>> Get()
        {
            var categorias = await context.Categorias.ToListAsync();
            var dtos = mapper.Map<List<CategoriaDTO>>(categorias);

            return dtos;
        }

        [HttpGet ("{id:int}", Name = "obtenerCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var categorias = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

            if (categorias == null)
            {
                return NotFound();
            }

            var dtos = mapper.Map<CategoriaDTO>(categorias);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoriaCreacionDTO categoriaDTO)
        {
            var categoria = mapper.Map<Categoria>(categoriaDTO);

            if (categoria == null)
            {
                return NotFound();
            }

            context.Add(categoria);
            await context.SaveChangesAsync();

            var nuevaCategoria = mapper.Map<CategoriaDTO>(categoria);

            return new CreatedAtRouteResult("obtenerCategoria", new { id = categoria.Id }, nuevaCategoria);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaCreacionDTO>> Put([FromBody] CategoriaCreacionDTO categoriaDTO, int id)
        {
            var categoriaDB = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

            if (categoriaDB == null)
            {
                return NotFound();
            }

            categoriaDB = mapper.Map(categoriaDTO, categoriaDB);
            await context.SaveChangesAsync();

            var categoriaModificada = mapper.Map<CategoriaDTO>(categoriaDB);

            return new CreatedAtRouteResult("obtenerCategoria", new { id = categoriaDB.Id }, categoriaModificada);
        }

        [HttpPut("ganador/{categoriaId:int}/pelicula/{peliculaId:int}")]
        public async Task<ActionResult<CategoriaGanadorDTO>> Put([FromBody] CategoriaGanadorDTO categoriaGanadorDTO, 
            int categoriaId, int peliculaId)
        {
            var categoriaPelicula = await context.CategoriaPeliculas
                .FirstOrDefaultAsync(x => x.CategoriaId == categoriaId && x.PeliculaId == peliculaId);

            if (categoriaPelicula == null)
            {
                return NotFound();
            }

            categoriaPelicula.Ganador = true;
            context.Entry(categoriaPelicula).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("ganador/{CategoriaId:int}/actor/{ActorId:int}")]
        public async Task<ActionResult<CategoriaGanadorDTO>> PutCategoriaActor([FromBody] CategoriaGanadorDTO categoriaGanadorDTO, 
            int categoriaId, int actorId)
        {
            var categoriaActor = await context.CategoriasActores
                .FirstOrDefaultAsync(x => x.CategoriaId == categoriaId && x.ActorId == actorId);

            if (categoriaActor == null)
            {
                return NotFound();
            }

            categoriaActor.Ganador = true;
            context.Entry(categoriaActor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            context.Remove(categoria);
            await context.SaveChangesAsync();

            var categorias = await context.Categorias.ToListAsync();
            var dtos = mapper.Map<List<CategoriaDTO>>(categorias);

            return new CreatedAtRouteResult("obtenerCategorias", dtos);
        }
    }
}
