using ApiPeliculas.Controllers;
using ApiPeliculas.DTOs;
using ApiPeliculas.Entidades;
using NetTopologySuite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPITests.PruebasUnitarias
{
    public class SalasDeCineControllerTests: BasePruebas
    {
        [TestMethod]
        public async Task ObtenerSalasDeCineA5KilometrosOMenos()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            using (var context = LocalDbDatabaseInitializer.GetDbContextLocalDb(false))
            {
                var salasDeCine = new List<SalaDeCine>()
                {
                    new SalaDeCine()
                    {
                        Id = 1,
                        Nombre = "Cine Holea",
                        Ubicacion = geometryFactory.CreatePoint(new NetTopologySuite.Geometries.Coordinate(37.27231758753893, -6.920557848000785))
                    }
                };

                context.AddRange(salasDeCine);
                await context.SaveChangesAsync();
            }

            var filtro = new SalaDeCineCercanoFiltroDTO()
            {
                DistanciaEnKms = 5,
                Latitud = 37.262738126314744,
                Longitud = -6.933651721156828
            };

            using (var context = LocalDbDatabaseInitializer.GetDbContextLocalDb(false))
            {
                var mapper = ConfigurarAutoMapper();
                var controller = new SalasDeCineController(context, mapper, geometryFactory);
                var respuesta = await controller.Cercanos(filtro);
                var valor = respuesta.Value;
                Assert.AreEqual(2, valor.Count);
            }
        }
    }
}
