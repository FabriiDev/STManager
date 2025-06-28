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
    [Route("api/[controller]")] // url api/producto_tipo
    public class ProductoTipoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductoTipoController> _logger;

        public ProductoTipoController(ApplicationDbContext context, ILogger<ProductoTipoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/producto_tipo
        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductoTipo>>> GetProductoTipos(
            [FromQuery] int page = 1, // pagina por defecto 1
            [FromQuery] int pageSize = 10, // cantidad de registros por pagina por defecto 10
            [FromQuery] string? tipoProducto = null) // permite filtrar por tipoProducto
        {
            try{
                // comienzo con la query base
                var query = _context.ProductoTipos.AsQueryable();

                // filtramos los activos
                query = query.Where(pt => pt.Activo);

                // filtros personalizados
                if (!string.IsNullOrEmpty(tipoProducto))
                    query = query.Where(pt => pt.TipoProducto.ToLower().Contains(tipoProducto.ToLower()));

                // total de elementos filtrados antes de paginar
                var totalItems = await query.CountAsync();

                // hacemos la paginacion
                var items = await query
                    .Skip((page - 1) * pageSize) // salta los elementos de las paginas anteriores
                    .Take(pageSize) // toma los elementos de la pagina actual
                    .ToListAsync(); // convierte a lista

                // devolvemos los resultados paginados
                return new PagedResult<ProductoTipo>
                {
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize,
                    Items = items
                };
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los tipos de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/producto_tipo/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoTipo>> GetProductoTipoById(int id)
        {
            try{

                // buscamos el producto tipo por id
                var productoTipo = await _context.ProductoTipos.FindAsync(id);

                // si no existe, devolvemos NotFound
                if (productoTipo == null)
                    return NotFound();

                // devolvemos el producto tipo encontrado
                return productoTipo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el tipo de producto por id");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/producto_tipo
        [HttpPost]
        public async Task<ActionResult<ProductoTipo>> CreateProductoTipo(ProductoTipoCreateDto productoTipoDto)
        {
            try{
                // validamos el modelo
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // creamos una nueva instancia de ProductoTipo
                var productoTipo = new ProductoTipo
                {
                    TipoProducto = productoTipoDto.TipoProducto,
                    Activo = productoTipoDto.Activo
                };

                // agregamos el nuevo producto tipo al contexto
                _context.ProductoTipos.Add(productoTipo);
                await _context.SaveChangesAsync();

                // devolvemos el producto tipo creado con un 201 Created
                return CreatedAtAction(nameof(GetProductoTipoById), new { id = productoTipo.IdTipoProducto }, productoTipo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el tipo de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/producto_tipo/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProductoTipo(int id, ProductoTipoCreateDto productoTipoDto)
        {
            try{
                // validamos el modelo
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // buscamos el producto tipo por id
                var productoTipo = await _context.ProductoTipos.FindAsync(id);
                if (productoTipo == null)
                    return NotFound();

                // actualizamos los campos
                productoTipo.TipoProducto = productoTipoDto.TipoProducto;
                productoTipo.Activo = productoTipoDto.Activo;

                // guardamos los cambios
                _context.ProductoTipos.Update(productoTipo);
                await _context.SaveChangesAsync();

                // devolvemos NoContent para indicar que la actualización fue exitosa
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el tipo de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/producto_tipo/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProductoTipo(int id)
        {
            try{

                // buscamos el producto tipo por id
                var productoTipo = await _context.ProductoTipos.FindAsync(id);
                if (productoTipo == null)
                    return NotFound();

                // marcamos como inactivo en lugar de eliminar
                productoTipo.Activo = false;

                // guardamos los cambios
                _context.ProductoTipos.Update(productoTipo);
                await _context.SaveChangesAsync();

                // devolvemos NoContent para indicar que la eliminación fue exitosa
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el tipo de producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}