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
    [Route("api/[controller]")] // url api/impresionmaterial
    public class ImpresionMaterialController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ImpresionMaterialController> _logger;

        public ImpresionMaterialController(ApplicationDbContext context, ILogger<ImpresionMaterialController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/impresionmaterial
        [HttpGet]
        public async Task<ActionResult<PagedResult<ImpresionMaterial>>> GetImpresionMateriales(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] string? nombre = null) // permite filtrar por nombre
        {
            try
            {
                // comienzo con la query base
                var query = _context.ImpresionMateriales.AsQueryable();

                // filtramos los activos
                query = query.Where(i => i.Activo);

                // filtros personalizados
                if (!string.IsNullOrEmpty(nombre))
                    query = query.Where(i => i.Nombre.ToLower().Contains(nombre.ToLower()));

                // total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // hacemos la paginacion
                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<ImpresionMaterial>
                {
                    TotalItems = totalItems,
                    Items = items,
                    Page = page,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los materiales de impresión");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // get individual impresion material por id
        [HttpGet("{id}")]
        public async Task<ActionResult<ImpresionMaterial>> GetImpresionMaterial(int id)
        {
            try
            {
                var impresionMaterial = await _context.ImpresionMateriales.FindAsync(id);

                if (impresionMaterial == null || !impresionMaterial.Activo)
                {
                    return NotFound("Material de impresión no encontrado o inactivo.");
                }

                return Ok(impresionMaterial);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el material de impresión");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/impresionmaterial
        [HttpPost]
        public async Task<ActionResult<ImpresionMaterial>> CreateImpresionMaterial(ImpresionMaterialCreateDto materialDto)
        {
            try
            {
                if (materialDto == null)
                {
                    return BadRequest("El material de impresión no puede ser nulo.");
                }

                var impresionMaterial = new ImpresionMaterial
                {
                    Nombre = materialDto.Nombre,
                    TipoMaterial = materialDto.TipoMaterial,
                    Color = materialDto.Color,
                    Descripcion = materialDto.Descripcion,
                    Activo = materialDto.Activo
                };

                _context.ImpresionMateriales.Add(impresionMaterial);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetImpresionMaterial), new { id = impresionMaterial.IdMaterial }, impresionMaterial);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el material de impresión");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/impresionmaterial/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ImpresionMaterial>> UpdateImpresionMaterial(int id, ImpresionMaterialCreateDto materialDto)
        {
            try
            {
                if (materialDto == null)
                {
                    return BadRequest("El material de impresión no puede ser nulo.");
                }

                var impresionMaterial = await _context.ImpresionMateriales.FindAsync(id);
                if (impresionMaterial == null || !impresionMaterial.Activo)
                {
                    return NotFound("Material de impresión no encontrado o inactivo.");
                }

                // Actualizamos los campos del material
                impresionMaterial.Nombre = materialDto.Nombre;
                impresionMaterial.TipoMaterial = materialDto.TipoMaterial;
                impresionMaterial.Color = materialDto.Color;
                impresionMaterial.Descripcion = materialDto.Descripcion;
                impresionMaterial.Activo = materialDto.Activo;

                _context.ImpresionMateriales.Update(impresionMaterial);
                await _context.SaveChangesAsync();

                return Ok(impresionMaterial);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el material de impresión");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/impresionmaterial/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImpresionMaterial(int id)
        {
            try
            {
                var impresionMaterial = await _context.ImpresionMateriales.FindAsync(id);
                if (impresionMaterial == null || !impresionMaterial.Activo)
                {
                    return NotFound("Material de impresión no encontrado o inactivo.");
                }

                // Marcamos como inactivo en lugar de eliminar físicamente
                impresionMaterial.Activo = false;

                _context.ImpresionMateriales.Update(impresionMaterial);
                await _context.SaveChangesAsync();

                return NoContent(); // devuelve 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el material de impresión");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}