using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.Equipos; // para el modelo equipo
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
using api.Models._Dto.Equipos; // para el dto de equipo

namespace api.Controllers.Equipos
{
    [ApiController]
    [Route("api/[controller]")] // url api/equipotipo

    public class EquipoTipoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EquipoTipoController> _logger;

        public EquipoTipoController(ApplicationDbContext context, ILogger<EquipoTipoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/equipotipo
        [HttpGet]
        public async Task<ActionResult<PagedResult<EquipoTipo>>> GetEquipoTipos(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] string? Tipo = null) // permite filtrar por tipo de producto
        {
            try
            {
                // comienzo con la query base
                var query = _context.EquipoTipos.AsQueryable();

                // filtramos los activos
                query = query.Where(e => e.Activo);

                // filtros personalizados
                if (!string.IsNullOrEmpty(Tipo))
                    query = query.Where(e => e.Tipo.ToLower().Contains(Tipo.ToLower()));

                // total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // hacemos la paginacion
                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<EquipoTipo>
                {
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize,
                    Items = items
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los tipos de equipo");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/equipotipo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipoTipo>> GetEquipoTipo(int id)
        {
            try
            {
                var equipoTipo = await _context.EquipoTipos.FindAsync(id);
                if (equipoTipo == null || !equipoTipo.Activo)
                {
                    return NotFound(); // devuelve 404 si no se encuentra o no está activo
                }
                return Ok(equipoTipo); // devuelve 200 con el tipo de equipo
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el tipo de equipo con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/equipotipo
        [HttpPost]
        public async Task<ActionResult<EquipoTipo>> CreateEquipoTipo([FromBody] EquipoTipoCreateDto equipoTipoDto)
        {
            if (equipoTipoDto == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede ser nulo");
            }

            try
            {
                var equipoTipo = new EquipoTipo
                {
                    Tipo = equipoTipoDto.Tipo,
                    Activo = equipoTipoDto.Activo
                };

                _context.EquipoTipos.Add(equipoTipo);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEquipoTipo), new { id = equipoTipo.IdTipoEquipo }, equipoTipo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el tipo de equipo");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/equipotipo/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<EquipoTipo>> UpdateEquipoTipo(int id, [FromBody] EquipoTipoCreateDto equipoTipoDto)
        {
            if (equipoTipoDto == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede ser nulo");
            }

            try
            {
                var equipoTipo = await _context.EquipoTipos.FindAsync(id);
                if (equipoTipo == null || !equipoTipo.Activo)
                {
                    return NotFound(); // devuelve 404 si no se encuentra o no está activo
                }

                // Actualizamos los campos
                equipoTipo.Tipo = equipoTipoDto.Tipo;
                equipoTipo.Activo = equipoTipoDto.Activo;

                _context.EquipoTipos.Update(equipoTipo);
                await _context.SaveChangesAsync();

                return Ok(equipoTipo); // devuelve 200 con el tipo de equipo actualizado
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el tipo de equipo con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }   

        // DELETE: api/equipotipo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipoTipo(int id)
        {
            try
            {
                var equipoTipo = await _context.EquipoTipos.FindAsync(id);
                if (equipoTipo == null || !equipoTipo.Activo)
                {
                    return NotFound(); // devuelve 404 si no se encuentra o no está activo
                }

                // Marcamos como inactivo en lugar de eliminar
                equipoTipo.Activo = false;

                _context.EquipoTipos.Update(equipoTipo);
                await _context.SaveChangesAsync();

                return NoContent(); // devuelve 204 No Content al eliminar correctamente
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el tipo de equipo con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}