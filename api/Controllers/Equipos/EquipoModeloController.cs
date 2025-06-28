using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.Equipos; // para el modelo equipo
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
using api.Models._Dto.Equipos; // para el dto de equipo

namespace api.Controller.Equipos
{
    [ApiController]
    [Route("api/[controller]")] // url api/equipomodelo

    public class EquipoModeloController : ControllerBase
    {                   
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EquipoModeloController> _logger;

        public EquipoModeloController(ApplicationDbContext context, ILogger<EquipoModeloController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/equipomodelo
        [HttpGet]
        public async Task<ActionResult<PagedResult<EquipoModelo>>> GetEquipoModelos(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] string? modelo = null) // permite filtrar por modelo y marca
        {
            try
            {
                // comienzo con la query base
                var query = _context.EquipoModelos.AsQueryable();

                // filtramos los activos
                query = query.Where(em => em.Activo);

                // filtros personalizados
                if (!string.IsNullOrEmpty(modelo))
                    query = query.Where(em => em.Modelo.ToLower().Contains(modelo.ToLower()));


                // total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // hacemos la paginacion
                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<EquipoModelo>
                {
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize,
                    Items = items
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los modelos de equipo");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/equipomodelo/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EquipoModelo>> GetEquipoModelo(int id)
        {
            try
            {
                var equipoModelo = await _context.EquipoModelos
                    .FirstOrDefaultAsync(em => em.IdModeloEquipo == id && em.Activo);

                if (equipoModelo == null)
                {
                    return NotFound($"Modelo de equipo con ID {id} no encontrado o inactivo.");
                }

                return Ok(equipoModelo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el modelo de equipo");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/equipomodelo
        [HttpPost]
        public async Task<ActionResult<EquipoModelo>> CreateEquipoModelo([FromBody] EquipoModeloCreateDto equipoModeloDto)
        {
            if (equipoModeloDto == null)
            {
                return BadRequest("El modelo de equipo no puede ser nulo.");
            }

            try
            {
                // Validar que la marca existe
                var marca = await _context.EquipoMarcas.FindAsync(equipoModeloDto.IdMarcaEquipo);
                if (marca == null || !marca.Activo)
                {
                    return BadRequest($"La marca con ID {equipoModeloDto.IdMarcaEquipo} no existe o está inactiva.");
                }

                // Crear el nuevo modelo de equipo
                var equipoModelo = new EquipoModelo
                {
                    Modelo = equipoModeloDto.Modelo,
                    IdMarcaEquipo = equipoModeloDto.IdMarcaEquipo,
                    Activo = equipoModeloDto.Activo
                };

                _context.EquipoModelos.Add(equipoModelo);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEquipoModelo), new { id = equipoModelo.IdModeloEquipo }, equipoModelo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el modelo de equipo");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/equipomodelo/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEquipoModelo(int id,EquipoModeloCreateDto equipoModeloDto)
        {
            if (equipoModeloDto == null)
            {
                return BadRequest("El modelo de equipo no puede ser nulo.");
            }

            try
            {
                // Buscar el modelo de equipo existente
                var equipoModelo = await _context.EquipoModelos.FindAsync(id);
                if (equipoModelo == null || !equipoModelo.Activo)
                {
                    return NotFound($"Modelo de equipo con ID {id} no encontrado o inactivo.");
                }

                // Validar que la marca existe
                var marca = await _context.EquipoMarcas.FindAsync(equipoModeloDto.IdMarcaEquipo);
                if (marca == null || !marca.Activo)
                {
                    return BadRequest($"La marca con ID {equipoModeloDto.IdMarcaEquipo} no existe o está inactiva.");
                }

                // Actualizar los campos del modelo de equipo
                equipoModelo.Modelo = equipoModeloDto.Modelo;
                equipoModelo.IdMarcaEquipo = equipoModeloDto.IdMarcaEquipo;
                equipoModelo.Activo = equipoModeloDto.Activo;

                _context.EquipoModelos.Update(equipoModelo);
                await _context.SaveChangesAsync();

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el modelo de equipo");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/equipomodelo/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEquipoModelo(int id)
        {
            try
            {
                // Buscar el modelo de equipo existente
                var equipoModelo = await _context.EquipoModelos.FindAsync(id);
                if (equipoModelo == null || !equipoModelo.Activo)
                {
                    return NotFound($"Modelo de equipo con ID {id} no encontrado o inactivo.");
                }

                // Marcar como inactivo en lugar de eliminar
                equipoModelo.Activo = false;

                _context.EquipoModelos.Update(equipoModelo);
                await _context.SaveChangesAsync();

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el modelo de equipo");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }

    


}