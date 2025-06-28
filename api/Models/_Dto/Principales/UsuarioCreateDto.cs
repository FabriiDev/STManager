namespace api.Models._Dto.Principales
{
    public class UsuarioCreateDto
    {
        public string Email { get; set; } = string.Empty;
        public string Nick { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // Incluimos la contraseña para el registro
        public string Rol { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}