namespace ApiPeliculas.DTOs
{
    public class FiltroPeliculasDTO
    {
        public int Pagina { get; set; }
        public int CantidadRegistrosPorPagina { get; set; }
        public PaginacionDTO paginacion
        {
            get 
            {
                return new PaginacionDTO()
                {
                    Pagina = this.Pagina,
                    CantidadRegistrosPorPagina = this.CantidadRegistrosPorPagina
                };
            }
        }

        public string Titulo { get; set; }
        public int GeneroId { get; set; }
        public int NumOscars { get; set; }
        public string CampoOrdenar { get; set; }
        public Boolean OrdenAscendente { get; set; } = true;
    }
}
