namespace api.Models._Dto.Principales
{
    public class ClienteCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string Otros { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}
