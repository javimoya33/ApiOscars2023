using NetTopologySuite.Noding;

namespace ApiPeliculas.DTOs
{
    public class PeliculaNominadasDTO: PeliculaDTO
    {
        public bool Ganador { get; set; }
        public int Nominaciones { get; set; }
    }
}
