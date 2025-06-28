using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.Relaciones; // para el modelo relaciones
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
using api.Models._Dto.Relaciones; // para el dto de relaciones

namespace api.Controllers.Relaciones
{
    // get insesarios, solo put y post
    [ApiController]
    [Route("api/[controller]")] // url api/detalleordenproducto

    public class DetalleOrdenProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DetalleOrdenProductoController> _logger;

        public DetalleOrdenProductoController(ApplicationDbContext context, ILogger<DetalleOrdenProductoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST: api/detalleordenproducto
        [HttpPost]
        public async Task<ActionResult<DetalleOrdenProducto>> CreateDetalleOrdenProducto(DetalleOrdenProductoDto detalleDto)
        {
            try
            {
                var detalle = new DetalleOrdenProducto
                {
                    NroOrden = detalleDto.NroOrden,
                    IdProducto = detalleDto.IdProducto,
                    Cantidad = detalleDto.Cantidad,
                    PrecioUnitario = detalleDto.PrecioUnitario
                };

                _context.DetalleOrdenProductos.Add(detalle);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateDetalleOrdenProducto), new { id = detalle.NroOrden }, detalle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el detalle de la orden de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/detalleordenproducto/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDetalleOrdenProducto(int id, DetalleOrdenProductoDto detalleDto)
        {
            try
            {
                var detalle = await _context.DetalleOrdenProductos.FindAsync(id);
                if (detalle == null)
                {
                    return NotFound("Detalle de orden de producto no encontrado");
                }

                detalle.NroOrden = detalleDto.NroOrden;
                detalle.IdProducto = detalleDto.IdProducto;
                detalle.Cantidad = detalleDto.Cantidad;
                detalle.PrecioUnitario = detalleDto.PrecioUnitario;

                _context.DetalleOrdenProductos.Update(detalle);
                await _context.SaveChangesAsync();

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el detalle de la orden de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}