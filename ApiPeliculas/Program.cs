using ApiPeliculas;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Oscars}/{action=Peliculas}");

app.Run();
