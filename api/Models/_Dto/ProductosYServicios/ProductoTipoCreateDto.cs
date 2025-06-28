namespace api.Models._Dto.ProductosYServicios
{
    public class ProductoTipoCreateDto
    {
        public string TipoProducto { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}