using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models.Principales; // para el modelo Cliente
using Microsoft.EntityFrameworkCore;
using System.Linq; // para where, contains, etc.
using api.Models._Dto; // para los dto de paginas
using api.Models._Dto.Principales; // para el dto de user

namespace api.Controllers.Principales
{
    [ApiController]
    [Route("api/[controller]")] // url api/usuario

    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ApplicationDbContext context, ILogger<UsuarioController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/usuario

        [HttpGet]
        public async Task<ActionResult<PagedResult<UsuarioGetDto>>> GetUsuarios(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? nick = null,
            [FromQuery] string? rol = null)
        {
            try
            {
                var query = _context.Usuarios.AsQueryable();

                query = query.Where(u => u.Activo);

                if (!string.IsNullOrEmpty(nick))
                    query = query.Where(u => u.Nick.ToLower().Contains(nick.ToLower()));
                if (!string.IsNullOrEmpty(rol))
                    query = query.Where(u => u.Rol.ToLower().Contains(rol.ToLower()));

                var totalItems = await query.CountAsync();

                var items = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(u => new UsuarioGetDto
                    {
                        IdUsuario = u.IdUsuario,
                        Nick = u.Nick,
                        Rol = u.Rol,
                        Email = u.Email,
                        Activo = u.Activo
                    })
                    .ToListAsync();

                return Ok(new PagedResult<UsuarioGetDto>
                {
                    Items = items,
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los usuarios");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Get individual
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioGetDto>> GetUsuario(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null) return NotFound();

                var usuarioDto = new UsuarioGetDto
                {
                    IdUsuario = usuario.IdUsuario,
                    Email = usuario.Email,
                    Nick = usuario.Nick,
                    Rol = usuario.Rol,
                    Activo = usuario.Activo
                };

                return Ok(usuarioDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/usuario
        [HttpPost]
        public async Task<ActionResult<UsuarioGetDto>> PostUsuario(UsuarioCreateDto dto)
        {
            try
            {
                var usuario = new Usuario
                {
                    Email = dto.Email,
                    Nick = dto.Nick,
                    Password = dto.Password, // Asegúrate de hashear la contraseña antes de guardarla
                    Rol = dto.Rol,
                    Activo = dto.Activo
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                var usuarioDto = new UsuarioGetDto
                {
                    IdUsuario = usuario.IdUsuario,
                    Email = usuario.Email,
                    Nick = usuario.Nick,
                    Rol = usuario.Rol,
                    Activo = usuario.Activo
                };

                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuarioDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/usuario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioCreateDto dto)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null) return NotFound();

                usuario.Email = dto.Email;
                usuario.Nick = dto.Nick;
                usuario.Password = dto.Password; // Asegúrate de hashear la contraseña antes de guardarla
                usuario.Rol = dto.Rol;
                usuario.Activo = dto.Activo;

                _context.Entry(usuario).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el usuario con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/usuario/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null) return NotFound();

                usuario.Activo = false; // Cambia el estado a inactivo
                _context.Entry(usuario).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el usuario con ID {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}