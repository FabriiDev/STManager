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
    [Route("api/[controller]")] // url api/equipomarca

    public class EquipoMarcaController : ControllerBase
    {                   
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EquipoMarcaController> _logger;

        public EquipoMarcaController(ApplicationDbContext context, ILogger<EquipoMarcaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/equipomarca
        [HttpGet]
        public async Task<ActionResult<PagedResult<EquipoMarca>>> GetEquiposMarcas(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] string? marca = null) // permite filtrar por nombre de marca
        {
            try{
                // comienzo con la query base
                var query = _context.EquipoMarcas.AsQueryable();

                // filtramos los activos
                query = query.Where(e => e.Activo);

                // filtros personalizados
                if (!string.IsNullOrEmpty(marca))
                    query = query.Where(e => e.Marca.ToLower().Contains(marca.ToLower()));

                // total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // hacemos la paginacion
                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<EquipoMarca>
                {
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize,
                    Items = items
                };
            }catch(Exception ex){
                _logger.LogError(ex, "Error al obtener las marcas de equipos");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // get individual
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipoMarca>> GetEquipoMarca(int id)
        {
            try{
                var equipoMarca = await _context.EquipoMarcas.FindAsync(id);
                if (equipoMarca == null)
                    return NotFound(); // devuelve 404 si no se encuentra

                return Ok(equipoMarca); // devuelve 200 con el objeto encontrado
            }catch(Exception ex){
                _logger.LogError(ex, "Error al obtener la marca de equipo con id {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/equipomarca
        [HttpPost]
        public async Task<ActionResult<EquipoMarca>> CreateEquipoMarca(EquipoMarcaCreateDto equipoMarcaCreate)
        {
            if (equipoMarcaCreate == null)
                return BadRequest("El objeto no puede ser nulo");

            try{
                var equipoMarca = new EquipoMarca
                {
                    Marca = equipoMarcaCreate.Marca,
                    Activo = equipoMarcaCreate.Activo
                };

                _context.EquipoMarcas.Add(equipoMarca);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEquipoMarca), new { id = equipoMarca.IdMarcaEquipo }, equipoMarca);
            }catch(Exception ex){
                _logger.LogError(ex, "Error al crear la marca de equipo");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/equipomarca/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<EquipoMarca>> UpdateEquipoMarca(int id, EquipoMarcaCreateDto equipoMarcaUpdate)
        {
            if (equipoMarcaUpdate == null)
                return BadRequest("El objeto no puede ser nulo");

            try{
                var equipoMarca = await _context.EquipoMarcas.FindAsync(id);
                if (equipoMarca == null)
                    return NotFound(); // devuelve 404 si no se encuentra

                // actualizamos los campos
                equipoMarca.Marca = equipoMarcaUpdate.Marca;
                equipoMarca.Activo = equipoMarcaUpdate.Activo;

                _context.EquipoMarcas.Update(equipoMarca);
                await _context.SaveChangesAsync();

                return Ok(equipoMarca); // devuelve 200 con el objeto actualizado
            }catch(Exception ex){
                _logger.LogError(ex, "Error al actualizar la marca de equipo con id {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/equipomarca/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipoMarca(int id)
        {
            try{
                var equipoMarca = await _context.EquipoMarcas.FindAsync(id);
                if (equipoMarca == null)
                    return NotFound(); // devuelve 404 si no se encuentra

                // marcamos como inactivo en lugar de eliminar
                equipoMarca.Activo = false;

                _context.EquipoMarcas.Update(equipoMarca);
                await _context.SaveChangesAsync();

                return NoContent(); // devuelve 204 sin contenido
            }catch(Exception ex){
                _logger.LogError(ex, "Error al eliminar la marca de equipo con id {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}