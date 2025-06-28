using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.ProductosYServicios; // para el modelo ProductoMarca
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto
using api.Models._Dto.ProductosYServicios; // para el dto de ProductoMarca

namespace api.Controllers.ProductosYServicios
{
    [ApiController]
    [Route("api/[controller]")] // url api/producto_marca

    public class ProductoMarcaController : ControllerBase{

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductoMarcaController> _logger;

        public ProductoMarcaController(ApplicationDbContext context, ILogger<ProductoMarcaController> logger){
            _context = context;
            _logger = logger;
        }

        // GET: api/producto_marca
        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductoMarca>>> GetProductoMarcas(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] string? marcaProducto = null) // permite filtrar por marcaProducto
        {
            try{
                // comienzo con la query base
                var query = _context.ProductoMarcas.AsQueryable();

                // filtramos los activos
                query = query.Where(pm => pm.Activo);

                // filtros personalizados
                if (!string.IsNullOrEmpty(marcaProducto))
                    query = query.Where(pm => pm.MarcaProducto.ToLower().Contains(marcaProducto.ToLower()));

                // total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // hacemos la paginacion
                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<ProductoMarca>
                {
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize,
                    Items = items
                };
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las marcas de productos");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/producto_marca/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoMarca>> GetProductoMarcaById(int id)
        {
            try{
                var productoMarca = await _context.ProductoMarcas
                    .FirstOrDefaultAsync(pm => pm.IdMarcaProducto == id && pm.Activo);

                if (productoMarca == null)
                {
                    return NotFound();
                }

                return productoMarca;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la marca de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/producto_marca
        [HttpPost]
        public async Task<ActionResult<ProductoMarca>> PostProductoMarca(ProductoMarcaDto dto)
        {
            try{
                var marca = new ProductoMarca{
                    MarcaProducto = dto.MarcaProducto,
                    Activo = dto.Activo
                };
                _context.ProductoMarcas.Add(marca);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProductoMarcaById), new { id = marca.IdMarcaProducto }, marca);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la marca de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/producto_marca/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutProductoMarca(int id, ProductoMarcaDto dto)
        {
            try{
                var productoMarca = await _context.ProductoMarcas.FindAsync(id);
                if (productoMarca == null)
                {
                    return NotFound();
                }

                productoMarca.MarcaProducto = dto.MarcaProducto;
                productoMarca.Activo = dto.Activo;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la marca de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/producto_marca/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProductoMarca(int id)
        {
            try{
                var productoMarca = await _context.ProductoMarcas.FindAsync(id);
                if (productoMarca == null)
                {
                    return NotFound();
                }

                // en vez de eliminar, marcamos como inactivo
                productoMarca.Activo = false;
                await _context.SaveChangesAsync();

                return NoContent();
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la marca de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}