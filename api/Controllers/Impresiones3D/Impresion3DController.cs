using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.Impresiones3D; // para el modelo impresion
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
using api.Models._Dto.Impresiones3D; // para el dto de impresion

namespace api.Controllers.Impresiones3D
{
    [ApiController]
    [Route("api/[controller]")] // url api/impresion3d

    public class Impresion3DController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<Impresion3DController> _logger;

        public Impresion3DController(ApplicationDbContext context, ILogger<Impresion3DController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/impresion3d
        [HttpGet]
        public async Task<ActionResult<PagedResult<Impresion3D>>> GetImpresiones3D(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] string? descripcion = null,
            [FromQuery] string? estado = null) // permite filtrar por descripcion y estado
        {
            try
            {
                // comienzo con la query base
                var query = _context.Impresiones3D.AsQueryable();

                // filtramos los activos
                query = query.Where(i => i.Activo);

                // filtros personalizados
                if (!string.IsNullOrEmpty(descripcion))
                    query = query.Where(i => i.Descripcion.ToLower().Contains(descripcion.ToLower()));

                if (!string.IsNullOrEmpty(estado))
                    query = query.Where(i => i.Estado.ToLower() == estado.ToLower());

                // total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // hacemos la paginacion
                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<Impresion3D>
                {
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize,
                    Items = items
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las impresiones 3D");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/impresion3d/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Impresion3D>> GetImpresion3D(int id)
        {
            try
            {
                var impresion = await _context.Impresiones3D
                    .Include(i => i.Cliente) // incluir cliente relacionado
                    .Include(i => i.Venta) // incluir venta relacionada si existe
                    .Include(i => i.Items) // incluir items de la impresion
                    .FirstOrDefaultAsync(i => i.IdImpresion3D == id && i.Activo);

                if (impresion == null)
                {
                    return NotFound("Impresión 3D no encontrada o inactiva.");
                }

                return Ok(impresion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la impresión 3D");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/impresion3d
        [HttpPost]
        public async Task<ActionResult<Impresion3D>> CreateImpresion3D([FromBody] Impresion3DCreateDto impresionDto)
        {
            if (impresionDto == null)
            {
                return BadRequest("Datos de impresión 3D no válidos.");
            }

            try
            {
                // mapeamos el dto al modelo
                var impresion = new Impresion3D
                {
                    IdCliente = impresionDto.IdCliente,
                    IdVenta = impresionDto.IdVenta,
                    FechaIngreso = impresionDto.FechaIngreso,
                    FechaEstimadaEntrega = impresionDto.FechaEstimadaEntrega,
                    FechaEntrega = impresionDto.FechaEntrega,
                    Descripcion = impresionDto.Descripcion,
                    Total = impresionDto.Total,
                    Estado = impresionDto.Estado,
                    Entregada = impresionDto.Entregada,
                    Activo = impresionDto.Activo
                };

                // agregamos la nueva impresion al contexto
                _context.Impresiones3D.Add(impresion);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetImpresion3D), new { id = impresion.IdImpresion3D }, impresion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la impresión 3D");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/impresion3d/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Impresion3D>> UpdateImpresion3D(int id, [FromBody] Impresion3DCreateDto impresionDto)
        {
            if (impresionDto == null)
            {
                return BadRequest("Datos de impresión 3D no válidos.");
            }

            try
            {
                var impresion = await _context.Impresiones3D.FindAsync(id);
                if (impresion == null || !impresion.Activo)
                {
                    return NotFound("Impresión 3D no encontrada o inactiva.");
                }

                // actualizamos los campos
                impresion.IdCliente = impresionDto.IdCliente;
                impresion.IdVenta = impresionDto.IdVenta;
                impresion.FechaIngreso = impresionDto.FechaIngreso;
                impresion.FechaEstimadaEntrega = impresionDto.FechaEstimadaEntrega;
                impresion.FechaEntrega = impresionDto.FechaEntrega;
                impresion.Descripcion = impresionDto.Descripcion;
                impresion.Total = impresionDto.Total;
                impresion.Estado = impresionDto.Estado;
                impresion.Entregada = impresionDto.Entregada;
                impresion.Activo = impresionDto.Activo;

                // guardamos los cambios
                _context.Impresiones3D.Update(impresion);
                await _context.SaveChangesAsync();

                return Ok(impresion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la impresión 3D");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/impresion3d/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteImpresion3D(int id)
        {
            try
            {
                var impresion = await _context.Impresiones3D.FindAsync(id);
                if (impresion == null || !impresion.Activo)
                {
                    return NotFound("Impresión 3D no encontrada o inactiva.");
                }

                // marcamos como inactiva en lugar de eliminar
                impresion.Activo = false;
                _context.Impresiones3D.Update(impresion);
                await _context.SaveChangesAsync();

                return NoContent(); // devuelve 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la impresión 3D");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}