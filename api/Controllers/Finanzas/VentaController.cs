
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.Finanzas; // para el modelo venta
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
using api.Models._Dto.Finanzas; // para el dto de venta

namespace api.Controtrollers.Finanzas
{
    [ApiController]
    [Route("api/[controller]")] // url api/venta
    public class VentaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VentaController> _logger;

        public VentaController(ApplicationDbContext context, ILogger<VentaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/venta
        [HttpGet]
        public async Task<ActionResult<PagedResult<Venta>>> GetVentas(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] bool? estado = null) // permite filtrar por estado
        {
            try
            {
                // comienzo con la query base
                var query = _context.Ventas.AsQueryable();
                // filtros personalizados
                if (estado.HasValue)
                    query = query.Where(v => v.Estado == estado.Value);


                // total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // hacemos la paginacion
                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<Venta>
                {
                    TotalItems = totalItems,
                    Items = items,
                    Page = page,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las ventas");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // get individual por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> GetVentaById(int id)
        {
            try
            {
                var venta = await _context.Ventas
                    .Include(v => v.Usuario) // incluir usuario relacionado
                    .Include(v => v.Caja) // incluir caja relacionada
                    .Include(v => v.MetodoPago) // incluir metodo de pago relacionado
                    .FirstOrDefaultAsync(v => v.IdVenta == id);

                if (venta == null)
                {
                    return NotFound($"Venta con ID {id} no encontrada.");
                }

                return Ok(venta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la venta por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/venta
        [HttpPost]
        public async Task<ActionResult<Venta>> CreateVenta([FromBody] VentaCreateDto ventaDto)
        {
            if (ventaDto == null || ventaDto.IdUsuario <= 0 || ventaDto.IdCaja <= 0 || ventaDto.IdMetodoPago <= 0)
            {
                return BadRequest("Los datos de la venta no pueden ser nulos o inválidos.");
            }

            try
            {
                var venta = new Venta
                {
                    FechaVenta = ventaDto.FechaVenta,
                    Total = ventaDto.Total,
                    IdUsuario = ventaDto.IdUsuario,
                    IdCaja = ventaDto.IdCaja,
                    Estado = ventaDto.Estado,
                    IdMetodoPago = ventaDto.IdMetodoPago,
                    Activo = ventaDto.Activo
                };

                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetVentaById), new { id = venta.IdVenta }, venta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la venta");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/venta/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Venta>> UpdateVenta(int id, [FromBody] VentaCreateDto ventaDto)
        {
            if (ventaDto == null || ventaDto.IdUsuario <= 0 || ventaDto.IdCaja <= 0 || ventaDto.IdMetodoPago <= 0)
            {
                return BadRequest("Los datos de la venta no pueden ser nulos o inválidos.");
            }

            try
            {
                var venta = await _context.Ventas.FindAsync(id);
                if (venta == null)
                {
                    return NotFound($"Venta con ID {id} no encontrada.");
                }

                venta.FechaVenta = ventaDto.FechaVenta;
                venta.Total = ventaDto.Total;
                venta.IdUsuario = ventaDto.IdUsuario;
                venta.IdCaja = ventaDto.IdCaja;
                venta.Estado = ventaDto.Estado;
                venta.IdMetodoPago = ventaDto.IdMetodoPago;
                venta.Activo = ventaDto.Activo;

                _context.Ventas.Update(venta);
                await _context.SaveChangesAsync();

                return Ok(venta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la venta");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/venta/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            try
            {
                var venta = await _context.Ventas.FindAsync(id);
                if (venta == null)
                {
                    return NotFound($"Venta con ID {id} no encontrada.");
                }

                // Marcar como inactivo en lugar de eliminar
                venta.Activo = false;
                _context.Ventas.Update(venta);
                await _context.SaveChangesAsync();

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la venta");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}