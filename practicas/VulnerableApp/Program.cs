using Microsoft.EntityFrameworkCore;
using Serilog;
using VulnerableApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Serilog ahora se configura leyendo la sección "Serilog" de appsettings.json
// (nivel mínimo, sinks y enrichers), en vez de estar fijo en código.
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSession();

var app = builder.Build();

// Middleware de Serilog: registra automáticamente cada petición HTTP
// (método, ruta, código de estado y tiempo de respuesta) como complemento
// a los logs manuales de cada acción de los controladores.
app.UseSerilogRequestLogging();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

Log.Information("La aplicación VulnerableApp ha iniciado en el entorno: {Environment}", app.Environment.EnvironmentName);

app.Run();
