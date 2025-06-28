namespace api.Models._Dto.Impresiones3D
{
    public class ImpresionMaterialCreateDto
    {
        public string Nombre { get; set; } = string.Empty; // Nombre del material, no puede ser nulo
        public string TipoMaterial { get; set; } = string.Empty; // Tipo de material, no puede ser nulo
        public string Color { get; set; } = string.Empty; // Color del material, no puede ser nulo
        public string? Descripcion { get; set; } = string.Empty; // Descripción del material, puede ser nula
        public bool Activo { get; set; } = true; // Indica si el material está activo, por defecto es true
    }
}