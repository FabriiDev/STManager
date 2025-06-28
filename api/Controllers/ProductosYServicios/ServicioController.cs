using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.ProductosYServicios; // para el modelo servicio
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto
using api.Models._Dto.ProductosYServicios; // para el dto de ServicioCreateDto

namespace api.Controllers.ProductosYServicios
{
    [ApiController]
    [Route("api/[controller]")] // url api/servicio
    public class ServicioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ServicioController> _logger;

        public ServicioController(ApplicationDbContext context, ILogger<ServicioController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/servicio
        [HttpGet]
        public async Task<ActionResult<PagedResult<Servicio>>> GetServicios(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] string? nombre = null) // permite filtrar por nombre
        {
            try
            {   
                    // comienzo con la query base
                    var query = _context.Servicios.AsQueryable();

                    // filtramos los activos
                    query = query.Where(s => s.Activo);

                    // filtros personalizados
                    if (!string.IsNullOrEmpty(nombre))
                        query = query.Where(s => s.Nombre.ToLower().Contains(nombre.ToLower()));

                    // total de elementos filtrados antes de paginar
                    var totalItems = await query.CountAsync();

                    // hacemos la paginacion
                    var items = await query
                        .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                        .Take(pageSize) // toma los elementos de la pagina actual
                        .ToListAsync(); // convierte a lista

                    // devolvemos los resultados paginados
                return new PagedResult<Servicio>
                {
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize,
                    Items = items
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los servicios"); // back
                return StatusCode(500, "Ocurrió un error inesperado"); // front
            }
        }

        // GET: api/servicio/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Servicio>> GetServicioById(int id)
        {
            try{

                var servicio = await _context.Servicios.FindAsync(id);

                if (servicio == null || !servicio.Activo)
                    return NotFound();

                return servicio;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el servicio por id"); // back
                return StatusCode(500, "Ocurrió un error inesperado"); // front
            }
        }

        // POST: api/servicio 
        [HttpPost]
        public async Task<ActionResult<Servicio>> CreateServicio(ServicioCreateDto servicioDto)
        {
            try{
                // Validar el modelo
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Crear una nueva instancia de Servicio
                var servicio = new Servicio
                {
                    Nombre = servicioDto.Nombre,
                    Descripcion = servicioDto.Descripcion,
                    Precio = servicioDto.Precio,
                    Activo = servicioDto.Activo
                };

                // Agregar el nuevo servicio al contexto
                _context.Servicios.Add(servicio);
                await _context.SaveChangesAsync();

                // Devolver el servicio creado con un código 201 Created
                return CreatedAtAction(nameof(GetServicioById), new { id = servicio.IdServicio }, servicio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el servicio"); // back
                return StatusCode(500, "Ocurrió un error inesperado"); // front
            }
        }

        // PUT: api/servicio/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Servicio>> UpdateServicio(int id, ServicioCreateDto servicioDto)
        {
            try{

                // Validar el modelo
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Buscar el servicio por id
                var servicio = await _context.Servicios.FindAsync(id);
                if (servicio == null || !servicio.Activo)
                    return NotFound();

                // Actualizar los campos del servicio
                servicio.Nombre = servicioDto.Nombre;
                servicio.Descripcion = servicioDto.Descripcion;
                servicio.Precio = servicioDto.Precio;
                servicio.Activo = servicioDto.Activo;

                // Guardar los cambios en la base de datos
                _context.Servicios.Update(servicio);
                await _context.SaveChangesAsync();

                // Devolver el servicio actualizado
                return servicio;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el servicio"); // back
                return StatusCode(500, "Ocurrió un error inesperado"); // front
            }
        }

        // DELETE: api/servicio/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            try{
                // Buscar el servicio por id
                var servicio = await _context.Servicios.FindAsync(id);
                if (servicio == null || !servicio.Activo)
                    return NotFound();

                // Marcar el servicio como inactivo
                servicio.Activo = false;

                // Guardar los cambios en la base de datos
                _context.Servicios.Update(servicio);
                await _context.SaveChangesAsync();

                // Devolver NoContent para indicar que la eliminación fue exitosa
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el servicio"); // back
                return StatusCode(500, "Ocurrió un error inesperado"); // front
            }
        }
    }
}