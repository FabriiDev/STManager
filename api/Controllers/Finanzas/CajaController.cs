using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.Finanzas; // para el modelo caja
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
using api.Models._Dto.Finanzas; // para el dto de caja
using System;

namespace api.Controllers.Finanzas
{
    [ApiController]
    [Route("api/[controller]")] // url api/caja

    public class CajaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CajaController> _logger;

        public CajaController(ApplicationDbContext context, ILogger<CajaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/caja
        [HttpGet]
        public async Task<ActionResult<PagedResult<Caja>>> GetCajas(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10 // cantidad de registros por pagina por defecto 10
            ) 
        {
            try
            {
                // comienzo con la query base
                var query = _context.Cajas.AsQueryable();
                // filtros personalizados

                // total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // hacemos la paginacion
                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<Caja>
                {
                    TotalItems = totalItems,
                    Items = items,
                    Page = page,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las cajas");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // get individual por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Caja>> GetCaja(int id)
        {
            try
            {
                var caja = await _context.Cajas.FindAsync(id);
                if (caja == null)
                {
                    return NotFound(); // devuelve 404 si no se encuentra la caja
                }
                return Ok(caja); // devuelve 200 con la caja encontrada
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la caja con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/caja
        [HttpPost]
        public async Task<ActionResult<Caja>> CreateCaja([FromBody] CajaCreateDto cajaDto)
        {
            if (cajaDto == null || cajaDto.IdUsuarioApertura <= 0)
            {
                return BadRequest("La caja no puede ser nula y el ID del usuario de apertura debe ser válido.");
            }

            try
            {
                var caja = new Caja
                {
                    FechaApertura = cajaDto.FechaApertura,
                    FechaCierre = cajaDto.FechaCierre,
                    TotalVentas = cajaDto.TotalVentas,
                    TotalOrdenes = cajaDto.TotalOrdenes,
                    Observaciones = cajaDto.Observaciones,
                    IdUsuarioApertura = cajaDto.IdUsuarioApertura,
                    IdUsuarioCierre = cajaDto.IdUsuarioCierre
                };

                _context.Cajas.Add(caja);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCaja), new { id = caja.IdCaja }, caja); // devuelve 201 con la nueva caja creada
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la caja");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/caja/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Caja>> UpdateCaja(int id, [FromBody] CajaCreateDto cajaDto)
        {
            if (cajaDto == null || cajaDto.IdUsuarioApertura <= 0)
            {
                return BadRequest("La caja no puede ser nula y el ID del usuario de apertura debe ser válido.");
            }

            try
            {
                var caja = await _context.Cajas.FindAsync(id);
                if (caja == null)
                {
                    return NotFound("Caja no encontrada.");
                }

                // Actualizar los campos de la caja
                caja.FechaApertura = cajaDto.FechaApertura;
                caja.FechaCierre = cajaDto.FechaCierre;
                caja.TotalVentas = cajaDto.TotalVentas;
                caja.TotalOrdenes = cajaDto.TotalOrdenes;
                caja.Observaciones = cajaDto.Observaciones;
                caja.IdUsuarioApertura = cajaDto.IdUsuarioApertura;
                caja.IdUsuarioCierre = cajaDto.IdUsuarioCierre;

                _context.Cajas.Update(caja);
                await _context.SaveChangesAsync();

                return Ok(caja); // devuelve 200 con la caja actualizada
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la caja con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // no veo necesidad de un DELETE para caja, ya que no se elimina una caja, solo se cierra
    }
}
