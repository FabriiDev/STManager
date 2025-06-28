namespace api.Models._Dto.Impresiones3D
{
    public class ImpresionItemCreateDto
    {
        public int IdImpresion3D { get; set; } // ID de la impresión 3D a la que pertenece este item, no puede ser nulo
        public string Nombre { get; set; } = string.Empty; // Nombre del item, no puede ser nulo
        public string Descripcion { get; set; } = string.Empty; // Descripción del item, no puede ser nulo
        public int Cantidad { get; set; } = 1; // Cantidad del item, por defecto es 1
        public int IdMaterial { get; set; } // ID del material utilizado, no puede ser nulo
        public decimal PrecioUnitario { get; set; } = 0.0m; // Precio unitario del item, por defecto es 0.0
    }
}