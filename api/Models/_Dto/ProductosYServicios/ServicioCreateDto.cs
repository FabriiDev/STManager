namespace api.Models._Dto.ProductosYServicios
{
    public class ServicioCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public bool Activo { get; set; } = true;
    }
}