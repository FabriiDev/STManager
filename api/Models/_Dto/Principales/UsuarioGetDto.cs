namespace api.Models._Dto.Principales{
    public class UsuarioGetDto
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Nick { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        // saco la pass por seguridad
    }
}