using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.Principales; // para el modelo Cliente
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
using api.Models._Dto.Principales; // para el dto de cliente

namespace api.Controllers.Principales
{
    [ApiController]
    [Route("api/[controller]")] // url api/cliente

    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(ApplicationDbContext context, ILogger<ClienteController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/cliente

        [HttpGet]
        public async Task<ActionResult<PagedResult<Cliente>>> GetClientes( // ahora con el paginado hay que devolver un PagedResult<Cliente> en vez de un IEnumerable<Cliente>
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] string? nombre = null,
            [FromQuery] string? apellido = null) // permite filtrar por nombre y apellido los fromquery
        {
            try{
                // comienzo con la query base
                var query = _context.Clientes.AsQueryable(); 

                // filtramos los activos

                query = query.Where(c => c.Activo); 

                // filtros personalizados
                if (!string.IsNullOrEmpty(nombre))
                    query = query.Where(c => c.Nombre.ToLower().Contains(nombre.ToLower()));
                if (!string.IsNullOrEmpty(apellido))
                    query = query.Where(c => c.Apellido.ToLower().Contains(apellido.ToLower()));

                // ------------ aca tambien habria que hacer los order by -----------------

                // total de elementos filtrados antes de paginar

                var totalItems = await query.CountAsync();

                // hacemos la paginacion

                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<Cliente>
                {
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize,
                    Items = items
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los clientes");
                return StatusCode(500, "Ocurrió un error inesperado");
            }

            
        }

        // POST: api/cliente/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClienteById(int id)
        {
            try{

                var cliente = await _context.Clientes.FindAsync(id); // busca por id, si no lo encuentra devuelve null 
                if (cliente == null) return NotFound();
                return cliente;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el cliente con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }


        // POST: api/cliente
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteCreateDto dto) // se usa dto para evitar enviar todo el modelo con su arbol
        {
            try{
                    var cliente = new Cliente{
                        Nombre = dto.Nombre,
                        Apellido = dto.Apellido,
                        Direccion = dto.Direccion,
                        Celular = dto.Celular,
                        Otros = dto.Otros,
                        Activo = dto.Activo
                    };

                    _context.Clientes.Add(cliente);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetClienteById), new { id = cliente.IdCliente }, cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el cliente");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/cliente/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteCreateDto dto)
        {
            try{
                var cliente = await _context.Clientes.FindAsync(id); // busca el cliente por id
                if (cliente == null) return NotFound();

                // actualiza los campos del cliente
                cliente.Nombre = dto.Nombre;
                cliente.Apellido = dto.Apellido;
                cliente.Direccion = dto.Direccion;
                cliente.Celular = dto.Celular;
                cliente.Otros = dto.Otros;

                await _context.SaveChangesAsync(); // guarda los cambios en la base de datos
                return NoContent(); // no devuelve si fue ok, solo devuelve 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el cliente con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }

        }

        // detele logico 
        // DELETE: api/cliente/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try{
                var cliente = await _context.Clientes.FindAsync(id); // busca el cliente por id
                if (cliente == null) return NotFound();

                cliente.Activo = false; // cambia el estado a inactivo
                await _context.SaveChangesAsync(); // guarda los cambios en la base de datos
                return NoContent(); // no devuelve si fue ok, solo devuelve 204 No Content
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el cliente con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}