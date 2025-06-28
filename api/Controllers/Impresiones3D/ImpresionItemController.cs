using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.Impresiones3D; // para el modelo impresion
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
using api.Models._Dto.Impresiones3D; // para el dto de impresion
using System;

namespace api.Controllers.Impresiones3D
{
    [ApiController]
    [Route("api/[controller]")] // url api/impresionitem
    public class ImpresionItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ImpresionItemController> _logger;

        public ImpresionItemController(ApplicationDbContext context, ILogger<ImpresionItemController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // impresion item no tiene activo, se hace borrado fisico

        // GET: api/impresionitem
        [HttpGet]
        public async Task<ActionResult<PagedResult<ImpresionItem>>> GetImpresionItems(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] int? idImpresion3D = null) // permite filtrar por id de impresion 3D
        {
            try
            {
                // comienzo con la query base
                var query = _context.ImpresionItems.AsQueryable();

                // filtros personalizados
                if (idImpresion3D.HasValue)
                    query = query.Where(i => i.IdImpresion3D == idImpresion3D.Value);

                // total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // hacemos la paginacion
                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<ImpresionItem>
                {
                    TotalItems = totalItems,
                    Items = items,
                    Page = page,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los items de impresion");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // get individual impresion item por id
        [HttpGet("{id}")]
        public async Task<ActionResult<ImpresionItem>> GetImpresionItem(int id)
        {
            try
            {
                var item = await _context.ImpresionItems.FindAsync(id);
                if (item == null)
                {
                    return NotFound();
                }
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el item de impresion con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/impresionitem
        [HttpPost]
        public async Task<ActionResult<ImpresionItem>> CreateImpresionItem(ImpresionItemCreateDto itemDto)
        {
            try
            {
                if (itemDto == null)
                {
                    return BadRequest("El item de impresion no puede ser nulo");
                }

                var item = new ImpresionItem
                {
                    IdImpresion3D = itemDto.IdImpresion3D,
                    Nombre = itemDto.Nombre,
                    Descripcion = itemDto.Descripcion,
                    Cantidad = itemDto.Cantidad,
                    IdMaterial = itemDto.IdMaterial,
                    PrecioUnitario = itemDto.PrecioUnitario
                };

                _context.ImpresionItems.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetImpresionItem), new { id = item.IdImpresionItem }, item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el item de impresion");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/impresionitem/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ImpresionItem>> UpdateImpresionItem(int id, ImpresionItemCreateDto itemDto)
        {
            try
            {
                if (itemDto == null)
                {
                    return BadRequest("El item de impresion no puede ser nulo");
                }

                var item = await _context.ImpresionItems.FindAsync(id);
                if (item == null)
                {
                    return NotFound();
                }

                // actualizamos los campos del item
                item.IdImpresion3D = itemDto.IdImpresion3D;
                item.Nombre = itemDto.Nombre;
                item.Descripcion = itemDto.Descripcion;
                item.Cantidad = itemDto.Cantidad;
                item.IdMaterial = itemDto.IdMaterial;
                item.PrecioUnitario = itemDto.PrecioUnitario;

                _context.ImpresionItems.Update(item);
                await _context.SaveChangesAsync();

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el item de impresion con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // borrado fisico
        // DELETE: api/impresionitem/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImpresionItem(int id)
        {
            try
            {
                var item = await _context.ImpresionItems.FindAsync(id);
                if (item == null)
                {
                    return NotFound();
                }

                _context.ImpresionItems.Remove(item);
                await _context.SaveChangesAsync();

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el item de impresion con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}