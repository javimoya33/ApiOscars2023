using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertarParametrosPaginacion<T>(this HttpContext httpContext, IQueryable<T> queryable, 
            int cantidadRegistrosPorPagina)
        {
            double cantidad = await queryable.CountAsync();
            double cantidadPaginas = Math.Ceiling(cantidad / cantidadRegistrosPorPagina);
            int cantidadPaginasRedondeada = (int)cantidadPaginas;
            httpContext.Response.Headers.Add("cantidadPaginas", cantidadPaginasRedondeada.ToString());
        }
    }
}
