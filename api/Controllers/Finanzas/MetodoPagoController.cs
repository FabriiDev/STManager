using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.Finanzas; // para el modelo metodopago
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
using api.Models._Dto.Finanzas; // para el dto de metodopago


// sin get individual ni delete, solo get, post y put
namespace api.Controllers.Finanzas
{
    [ApiController]
    [Route("api/[controller]")] // url api/metodopago

    public class MetodoPagoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MetodoPagoController> _logger;

        public MetodoPagoController(ApplicationDbContext context, ILogger<MetodoPagoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/metodopago
        [HttpGet]
        public async Task<ActionResult<PagedResult<MetodoPago>>> GetMetodosPago(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] string? metodo = null) // permite filtrar por metodo
        {
            try
            {
                // comienzo con la query base
                var query = _context.MetodosPago.AsQueryable();
                // filtros personalizados
                if (!string.IsNullOrEmpty(metodo))
                    query = query.Where(m => m.Metodo.ToLower().Contains(metodo.ToLower()));

                // total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // hacemos la paginacion
                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<MetodoPago>
                {
                    TotalItems = totalItems,
                    Items = items,
                    Page = page,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los metodos de pago");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/metodopago
        [HttpPost]
        public async Task<ActionResult<MetodoPago>> CreateMetodoPago([FromBody] MetodoPagoCreateDto metodoPagoDto)
        {
            if (metodoPagoDto == null || string.IsNullOrEmpty(metodoPagoDto.Metodo))
            {
                return BadRequest("El método de pago no puede ser nulo o vacío.");
            }

            try
            {
                var metodoPago = new MetodoPago
                {
                    Metodo = metodoPagoDto.Metodo,
                };

                _context.MetodosPago.Add(metodoPago);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMetodosPago), new { id = metodoPago.IdMetodoPago }, metodoPago);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el método de pago");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/metodopago/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<MetodoPago>> UpdateMetodoPago(int id, [FromBody] MetodoPagoCreateDto metodoPagoDto)
        {
            if (metodoPagoDto == null || string.IsNullOrEmpty(metodoPagoDto.Metodo))
            {
                return BadRequest("El método de pago no puede ser nulo o vacío.");
            }

            try
            {
                var metodoPago = await _context.MetodosPago.FindAsync(id);
                if (metodoPago == null)
                {
                    return NotFound("Método de pago no encontrado.");
                }

                metodoPago.Metodo = metodoPagoDto.Metodo;
                _context.MetodosPago.Update(metodoPago);
                await _context.SaveChangesAsync();

                return Ok(metodoPago);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el método de pago");
                return StatusCode(500, "Error interno del servidor");
            }
        }


    }
}