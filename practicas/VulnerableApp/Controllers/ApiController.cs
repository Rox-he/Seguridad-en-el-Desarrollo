using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VulnerableApp.Data;

namespace VulnerableApp.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ApiController> _logger;

        public ApiController(AppDbContext db, ILogger<ApiController> logger)
        {
            _db = db;
            _logger = logger;
        }

        private string? ClientIp => HttpContext.Connection.RemoteIpAddress?.ToString();

        [HttpGet("user/{id}")]
        public IActionResult GetUser(int id)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation("Inicio Api.GetUser. Id:{Id} IP:{IP}", id, ClientIp);

            try
            {
                var user = _db.Users.Find(id);
                if (user == null)
                {
                    _logger.LogWarning("Api.GetUser: usuario no encontrado. Id:{Id} IP:{IP}", id, ClientIp);
                    return NotFound();
                }

                // No se registra el campo Password en el log, aunque la respuesta lo incluya.
                _logger.LogInformation(
                    "Api.GetUser exitoso. Id:{Id} Usuario:{Usuario} IP:{IP}",
                    id, user.Username, ClientIp);

                return Ok(new
                {
                    user.Id,
                    user.Username,
                    user.Email,
                    user.Balance,
                    user.Password
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en Api.GetUser. Id:{Id} IP:{IP}", id, ClientIp);
                throw;
            }
            finally
            {
                sw.Stop();
                _logger.LogInformation("Fin Api.GetUser. DuracionMs:{DuracionMs}", sw.ElapsedMilliseconds);
            }
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation("Inicio Api.GetAllUsers. IP:{IP}", ClientIp);

            try
            {
                var users = _db.Users.ToList();

                _logger.LogInformation(
                    "Fin Api.GetAllUsers. TotalUsuarios:{Total} IP:{IP} DuracionMs:{DuracionMs}",
                    users.Count, ClientIp, sw.ElapsedMilliseconds);

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en Api.GetAllUsers. IP:{IP}", ClientIp);
                throw;
            }
        }
    }
}
