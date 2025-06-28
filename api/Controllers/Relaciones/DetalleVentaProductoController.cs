using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.Relaciones; // para el modelo relaciones
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
using api.Models._Dto.Relaciones; // para el dto de relaciones

// sin gets solo put y post

namespace api.Controllers.Relaciones
{
    [ApiController]
    [Route("api/[controller]")] // url api/detalleventaproducto

    public class DetalleVentaProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DetalleVentaProductoController> _logger;

        public DetalleVentaProductoController(ApplicationDbContext context, ILogger<DetalleVentaProductoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST: api/detalleventaproducto
        [HttpPost]
        public async Task<ActionResult<DetalleVentaProducto>> CreateDetalleVentaProducto(DetalleVentaProductoDto detalleDto)
        {
            try
            {
                var detalle = new DetalleVentaProducto
                {
                    IdVenta = detalleDto.IdVenta,
                    IdProducto = detalleDto.IdProducto,
                    Cantidad = detalleDto.Cantidad,
                    PrecioUnitario = detalleDto.PrecioUnitario
                };

                _context.DetalleVentaProductos.Add(detalle);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateDetalleVentaProducto), new { id = detalle.IdDetalleVentaProducto }, detalle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el detalle de la venta de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/detalleventaproducto/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDetalleVentaProducto(int id, DetalleVentaProductoDto detalleDto)
        {
            try
            {
                var detalle = await _context.DetalleVentaProductos.FindAsync(id);
                if (detalle == null)
                {
                    return NotFound();
                }

                detalle.IdVenta = detalleDto.IdVenta;
                detalle.IdProducto = detalleDto.IdProducto;
                detalle.Cantidad = detalleDto.Cantidad;
                detalle.PrecioUnitario = detalleDto.PrecioUnitario;

                _context.Entry(detalle).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el detalle de la venta de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}