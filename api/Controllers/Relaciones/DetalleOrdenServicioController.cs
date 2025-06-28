using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.Relaciones; // para el modelo relaciones
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
using api.Models._Dto.Relaciones; // para el dto de relaciones

// sin get, solo put y post
namespace api.Controllers.Relaciones
{
    [ApiController]
    [Route("api/[controller]")] // url api/detalleordenservicio

    public class DetalleOrdenServicioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DetalleOrdenServicioController> _logger;

        public DetalleOrdenServicioController(ApplicationDbContext context, ILogger<DetalleOrdenServicioController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST: api/detalleordenservicio
        [HttpPost]
        public async Task<ActionResult<DetalleOrdenServicio>> CreateDetalleOrdenServicio(DetalleOrdenServicioDto detalleDto)
        {
            try
            {
                var detalle = new DetalleOrdenServicio
                {
                    IdOrden = detalleDto.IdOrden,
                    IdServicio = detalleDto.IdServicio,
                    PrecioUnitario = detalleDto.Precio
                };

                _context.DetalleOrdenServicios.Add(detalle);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateDetalleOrdenServicio), new { id = detalle.IdDetalleOrdenServicio }, detalle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el detalle de la orden de servicio");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/detalleordenservicio/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDetalleOrdenServicio(int id, DetalleOrdenServicioDto detalleDto)
        {
            try
            {
                var detalle = await _context.DetalleOrdenServicios.FindAsync(id);
                if (detalle == null)
                {
                    return NotFound();
                }

                detalle.IdOrden = detalleDto.IdOrden;
                detalle.IdServicio = detalleDto.IdServicio;
                detalle.PrecioUnitario = detalleDto.Precio;

                _context.Entry(detalle).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el detalle de la orden de servicio");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}