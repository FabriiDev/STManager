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
    [Route("api/[controller]")] // url api/equipo

    public class EquipoController : ControllerBase
    {                   
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EquipoController> _logger;

        public EquipoController(ApplicationDbContext context, ILogger<EquipoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/equipo
        [HttpGet]
        public async Task<ActionResult<PagedResult<Equipo>>> GetEquipos(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] string? numeroSerie = null,
            [FromQuery] int? IdEquipoMarca = null) // permite filtrar por numero de serie
        {
            try{
                // comienzo con la query base
                var query = _context.Equipos.AsQueryable();

                // filtramos los activos
                query = query.Where(e => e.Activo);

                // filtros personalizados
                if (!string.IsNullOrEmpty(numeroSerie))
                    query = query.Where(e => e.NumeroSerie.ToLower().Contains(numeroSerie.ToLower()));

                // total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // hacemos la paginacion
                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<Equipo>
                {
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize,
                    Items = items
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener equipos");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/equipo/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Equipo>> GetEquipo(int id)
        {
            try
            {
                var equipo = await _context.Equipos
                    .Include(e => e.Cliente) // Incluye el cliente relacionado
                    .Include(e => e.EquipoTipo) // Incluye el tipo de equipo relacionado
                    .Include(e => e.EquipoMarca) // Incluye la marca de equipo relacionada
                    .Include(e => e.EquipoModelo) // Incluye el modelo de equipo relacionado
                    .FirstOrDefaultAsync(e => e.IdEquipo == id && e.Activo);

                if (equipo == null)
                {
                    return NotFound();
                }

                return Ok(equipo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el equipo con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/equipo
        [HttpPost]
        public async Task<ActionResult<Equipo>> CreateEquipo(EquipoCreateDto equipoDto)
        {
            try
            {
                if (equipoDto == null) return BadRequest("El equipo no puede ser nulo");

                var equipo = new Equipo
                {
                    NumeroSerie = equipoDto.NumeroSerie,
                    IdCliente = equipoDto.IdCliente,
                    IdTipoEquipo = equipoDto.IdTipoEquipo,
                    IdModeloEquipo = equipoDto.IdModeloEquipo,
                    Activo = equipoDto.Activo
                };

                _context.Equipos.Add(equipo);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEquipo), new { id = equipo.IdEquipo }, equipo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el equipo");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/equipo/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEquipo(int id, EquipoCreateDto equipoDto)
        {
            try
            {

                var equipo = await _context.Equipos.FindAsync(id);
                if (equipo == null) return NotFound("Equipo no encontrado");

                // Actualizamos los campos del equipo
                equipo.NumeroSerie = equipoDto.NumeroSerie;
                equipo.IdCliente = equipoDto.IdCliente;
                equipo.IdTipoEquipo = equipoDto.IdTipoEquipo;
                equipo.IdModeloEquipo = equipoDto.IdModeloEquipo;
                equipo.Activo = equipoDto.Activo;

                _context.Entry(equipo).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el equipo con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/equipo/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEquipo(int id)
        {
            try
            {
                var equipo = await _context.Equipos.FindAsync(id);
                if (equipo == null) return NotFound("Equipo no encontrado");

                // Marcamos el equipo como inactivo en lugar de eliminarlo
                equipo.Activo = false;

                _context.Entry(equipo).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el equipo con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}