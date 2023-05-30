﻿using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Entidades
{
    public class SalaDeCine
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public Point Ubicacion { get; set; }
        public List<PeliculasSalasDeCine> PeliculasSalasDeCine { get; set; }
    }
}
