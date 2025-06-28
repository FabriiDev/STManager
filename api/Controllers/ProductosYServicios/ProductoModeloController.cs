using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.ProductosYServicios; // para el modelo producto modelo
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto
using api.Models._Dto.ProductosYServicios; // para el dto de producto modelo

namespace api.Controllers.ProductosYServicios
{
    [ApiController]
    [Route("api/[controller]")] // url api/producto_marca

    public class ProductoModeloController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductoModeloController> _logger;

        public ProductoModeloController(ApplicationDbContext context, ILogger<ProductoModeloController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/producto_moodelo
        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductoModelo>>> GetProductoModelos(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] string? modeloProducto = null) // permite filtrar por modeloProducto
        {
            try{
                // comienzo con la query base
                var query = _context.ProductoModelos.AsQueryable();

                // filtramos los activos
                query = query.Where(pm => pm.Activo);

                // filtros personalizados
                if (!string.IsNullOrEmpty(modeloProducto))
                    query = query.Where(pm => pm.ModeloProducto.ToLower().Contains(modeloProducto.ToLower()));

                // total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // hacemos la paginacion
                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<ProductoModelo>
                {
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize,
                    Items = items
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los modelos de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/producto_modelo/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoModelo>> GetProductoModeloById(int id)
        {
            try{
                // buscamos el producto modelo por id
                var productoModelo = await _context.ProductoModelos
                    .FirstOrDefaultAsync(pm => pm.IdModeloProducto == id && pm.Activo);

                // si no lo encontramos, devolvemos NotFound
                if (productoModelo == null)
                    return NotFound();

                // devolvemos el producto modelo encontrado
                return Ok(productoModelo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el modelo de producto por id");
                return StatusCode(500, "Error interno del servidor");
            }
        }


        // POST: api/producto_modelo
        [HttpPost]
        public async Task<ActionResult<ProductoModelo>> CreateProductoModelo(ProductoModeloCreateDto productoModeloDto)
        {
            try{
                // validamos el modelo
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // creamos una nueva instancia de ProductoModelo
                var productoModelo = new ProductoModelo
                {
                    ModeloProducto = productoModeloDto.ModeloProducto,
                    IdMarcaProducto = productoModeloDto.IdMarcaProducto,
                    Activo = productoModeloDto.Activo
                };

                // agregamos el nuevo modelo al contexto
                _context.ProductoModelos.Add(productoModelo);
                await _context.SaveChangesAsync();

                // devolvemos el resultado con el nuevo modelo creado
                return CreatedAtAction(nameof(GetProductoModeloById), new { id = productoModelo.IdModeloProducto }, productoModelo);
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el modelo de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/producto_modelo/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProductoModelo(int id, ProductoModeloCreateDto productoModeloDto)
        {
            try{

                // validamos el modelo
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // buscamos el producto modelo por id
                var productoModelo = await _context.ProductoModelos.FindAsync(id);
                if (productoModelo == null)
                    return NotFound();

                // actualizamos los campos del modelo
                productoModelo.ModeloProducto = productoModeloDto.ModeloProducto;
                productoModelo.IdMarcaProducto = productoModeloDto.IdMarcaProducto;
                productoModelo.Activo = productoModeloDto.Activo;

                // guardamos los cambios en la base de datos
                _context.ProductoModelos.Update(productoModelo);
                await _context.SaveChangesAsync();

                // devolvemos NoContent para indicar que la actualización fue exitosa
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el modelo de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/producto_modelo/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProductoModelo(int id)
        {
            try{
                // buscamos el producto modelo por id
                var productoModelo = await _context.ProductoModelos.FindAsync(id);
                if (productoModelo == null)
                    return NotFound();

                // en vez de eliminar, marcamos como inactivo
                productoModelo.Activo = false;
                _context.ProductoModelos.Update(productoModelo);
                await _context.SaveChangesAsync();

                // devolvemos NoContent para indicar que la eliminación fue exitosa
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el modelo de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

    }

    
    
}