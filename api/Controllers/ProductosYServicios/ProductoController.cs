using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.ProductosYServicios; // para el modelo Producto
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
using api.Models._Dto.ProductosYServicios; // para el dto de producto


namespace api.Controller.ProductosYServicios
{
    [ApiController]
    [Route("api/[controller]")] // url api/producto

    public class ProductoController : ControllerBase
    {                   
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(ApplicationDbContext context, ILogger<ProductoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/producto
        [HttpGet]
        public async Task<ActionResult<PagedResult<Producto>>> GetProductos(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] string? descripcion = null) // permite filtrar por descripcion
        {
            try{
                // comienzo con la query base
                var query = _context.Productos.AsQueryable();

                // filtramos los activos
                query = query.Where(p => p.Activo);

                // filtros personalizados
                if (!string.IsNullOrEmpty(descripcion))
                    query = query.Where(p => p.Descripcion.ToLower().Contains(descripcion.ToLower()));

                // total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // hacemos la paginacion
                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<Producto>
                {
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize,
                    Items = items
                };
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los productos");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/producto/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> GetProductoById(int id)
        {
            try{

                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return NotFound();
                }
                return producto;
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el producto con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/producto
        [HttpPost]
        public async Task<ActionResult<Producto>> CreateProducto(ProductoCreateDto productoDto)
        {
            try{
                if (productoDto == null) return NotFound("El producto no puede ser nulo");

                var producto = new Producto
                {
                    Descripcion = productoDto.Descripcion,
                    Precio = productoDto.Precio,
                    Stock = productoDto.Stock,
                    Activo = productoDto.Activo,
                    IdTipoProducto = productoDto.IdTipoProducto,
                    IdImagenProducto = productoDto.idImagenProducto,
                    IdMarcaProducto = productoDto.IdMarcaProducto,
                    IdModeloProducto = productoDto.IdModeloProducto
                };

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProductoById), new { id = producto.IdTipoProducto }, producto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/producto/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProducto(int id, ProductoCreateDto productoDto)
        {
            try{
                if (id != productoDto.IdTipoProducto) return BadRequest("El ID del producto no coincide");

                var producto = await _context.Productos.FindAsync(id);
                if (producto == null) return NotFound();

                // Actualizamos los campos del producto
                producto.Descripcion = productoDto.Descripcion;
                producto.Precio = productoDto.Precio;
                producto.Stock = productoDto.Stock;
                producto.Activo = productoDto.Activo;
                producto.IdTipoProducto = productoDto.IdTipoProducto;
                producto.IdImagenProducto = productoDto.idImagenProducto;
                producto.IdMarcaProducto = productoDto.IdMarcaProducto;
                producto.IdModeloProducto = productoDto.IdModeloProducto;

                _context.Entry(producto).State = EntityState.Modified; // marca el estado como modificado
                await _context.SaveChangesAsync();

                return NoContent(); // devuelve 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el producto con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/producto/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try{
                var producto = await _context.Productos.FindAsync(id);
                if (producto == null) return NotFound();

                producto.Activo = false; // Cambia el estado a inactivo
                _context.Entry(producto).State = EntityState.Modified; // marca el estado como modificado
                await _context.SaveChangesAsync();

                return NoContent(); // devuelve 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el producto con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}