using Microsoft.EntityFrameworkCore;
using Serilog;
using VulnerableApp.Data;
using VulnerableApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Serilog se configura leyendo la sección "Serilog" de appsettings.json
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

// Orden del pipeline de middleware global (SEGG-U2-P3G-3):
// 1) CorrelationId: primero de todos, para que exista un identificador único
//    disponible en el LogContext durante el resto de la petición.
// 2) ExceptionHandling: envuelve todo lo que sigue para capturar cualquier
//    excepción no controlada, ya con el CorrelationId disponible.
// 3) RequestLogging: registra el resultado final de la petición (incluido
//    un posible 500 generado por el middleware de excepciones anterior).
app.UseCorrelationId();
app.UseGlobalExceptionHandling();
app.UseRequestLogging();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

Log.Information("La aplicación VulnerableApp ha iniciado en el entorno: {Environment}", app.Environment.EnvironmentName);

app.Run();
