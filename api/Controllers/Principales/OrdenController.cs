using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.Principales; // para el modelo orden
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
using api.Models._Dto.Principales; // para el dto de la orden


namespace api.Controllers.Principales
{
    [ApiController]
    [Route("api/[controller]")] // url api/orden
    public class OrdenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrdenController> _logger;

        public OrdenController(ApplicationDbContext context, ILogger<OrdenController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/orden
        [HttpGet]
        public async Task<ActionResult<PagedResult<Orden>>> GetOrdenes(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
            // realizar filtros
            )
        {
            try
            {
                var query = _context.Ordenes.AsQueryable();

                // Filtramos los activos
                query = query.Where(o => o.Activo);

                // Total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // Hacemos la paginación
                var items = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(new PagedResult<Orden>
                {
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize,
                    Items = items
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las órdenes");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // get individual orden by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Orden>> GetOrdenById(int id)
        {
            try
            {
                var orden = await _context.Ordenes.FindAsync(id);
                if (orden == null)
                {
                    return NotFound();
                }
                return Ok(orden);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la orden por ID");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/orden
        [HttpPost]
        public async Task<ActionResult<Orden>> CreateOrden(OrdenCreateDto ordenDto)
        {
            try
            {
                var orden = new Orden
                {
                    IdEquipo = ordenDto.IdEquipo,
                    IdCliente = ordenDto.IdCliente,
                    IdUsuarioCreador = ordenDto.IdUsuarioCreador,
                    IdUsuarioEntrega = ordenDto.IdUsuarioEntrega,
                    IdUsuarioAsignado = ordenDto.IdUsuarioAsignado,
                    Falla = ordenDto.Falla,
                    DetalleTecnico = ordenDto.DetalleTecnico,
                    FechaIngreso = ordenDto.FechaIngreso,
                    FechaEntrega = ordenDto.FechaEntrega,
                    FechaPresupuesto = ordenDto.FechaPresupuesto,
                    Cargador = ordenDto.Cargador,
                    Extras = ordenDto.Extras,
                    Diagnostico = ordenDto.Diagnostico,
                    Presupuesto = ordenDto.Presupuesto,
                    PagoDiagnostico = ordenDto.PagoDiagnostico,
                    Entregada = ordenDto.Entregada,
                    Total = ordenDto.Total,
                    Activo = ordenDto.Activo
                };

                _context.Ordenes.Add(orden);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOrdenById), new { id = orden.NroOrden }, orden);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la orden");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/orden/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrden(int id, OrdenCreateDto ordenDto)
        {
            try
            {
                var orden = await _context.Ordenes.FindAsync(id);
                if (orden == null)
                {
                    return NotFound();
                }

                // Actualizamos los campos de la orden
                orden.IdEquipo = ordenDto.IdEquipo;
                orden.IdCliente = ordenDto.IdCliente;
                orden.IdUsuarioCreador = ordenDto.IdUsuarioCreador;
                orden.IdUsuarioEntrega = ordenDto.IdUsuarioEntrega;
                orden.IdUsuarioAsignado = ordenDto.IdUsuarioAsignado;
                orden.Falla = ordenDto.Falla;
                orden.DetalleTecnico = ordenDto.DetalleTecnico;
                orden.FechaIngreso = ordenDto.FechaIngreso;
                orden.FechaEntrega = ordenDto.FechaEntrega;
                orden.FechaPresupuesto = ordenDto.FechaPresupuesto;
                orden.Cargador = ordenDto.Cargador;
                orden.Extras = ordenDto.Extras;
                orden.Diagnostico = ordenDto.Diagnostico;
                orden.Presupuesto = ordenDto.Presupuesto;
                orden.PagoDiagnostico = ordenDto.PagoDiagnostico;
                orden.Entregada = ordenDto.Entregada;
                orden.Total = ordenDto.Total;
                orden.Activo = ordenDto.Activo;

                _context.Entry(orden).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la orden");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/orden/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrden(int id)
        {
            try
            {
                var orden = await _context.Ordenes.FindAsync(id);
                if (orden == null)
                {
                    return NotFound();
                }

                // Cambiamos el estado a inactivo en lugar de eliminar físicamente
                orden.Activo = false;
                _context.Entry(orden).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent(); // Devuelve 204 No Content si fue exitoso
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la orden");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}