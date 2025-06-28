using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.Globales; // para el modelo de configuracion
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
// configuracion no tiene dto de creacion, solo se actualiza

namespace api.Controllers.Globales
{
    [ApiController]
    [Route("api/[controller]")] // url api/configuracion
    public class ConfiguracionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ConfiguracionController> _logger;

        public ConfiguracionController(ApplicationDbContext context, ILogger<ConfiguracionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/configuracion
        [HttpGet]
        public async Task<ActionResult<Configuracion>> GetConfiguracion()
        {
            try
            {
                var configuracion = await _context.Configuraciones.FirstOrDefaultAsync();
                if (configuracion == null)
                {
                    return NotFound("No se encontró la configuración.");
                }
                return Ok(configuracion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la configuración");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // no tiene ni post ni get individual solo trae y actualiza la unica que va a existir

        // PUT: api/configuracion
        [HttpPut]
        public async Task<IActionResult> UpdateConfiguracion([FromBody] Configuracion configuracion)
        {
            if (configuracion == null)
            {
                return BadRequest("La configuración no puede ser nula.");
            }

            try
            {
                var conf = await _context.Configuraciones.FindAsync(1);
                // siempre va a haber una sola configuracion, asi que mando por defecto el id 1
                // el findasync 1 obliga a que siempre se mande el id 1 y no respeta lo que venga en el body
                if (conf == null)
                {
                    return NotFound("No se encontró la configuración para actualizar.");
                } // esto pooor las dudas, aunque no deberia ser nula nunca la conf 

                // Actualizar los campos necesarios
                conf.NombreTaller = configuracion.NombreTaller;
                conf.DireccionTaller = configuracion.DireccionTaller;
                conf.ValorDiagnostico = configuracion.ValorDiagnostico;
                conf.UrlLogo = configuracion.UrlLogo;
                conf.TelefonoTaller = configuracion.TelefonoTaller;
                conf.Eslogan = configuracion.Eslogan;
                conf.TextoPie = configuracion.TextoPie;
                conf.TextoLegal = configuracion.TextoLegal;

                _context.Configuraciones.Update(conf);
                await _context.SaveChangesAsync();

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la configuración");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}