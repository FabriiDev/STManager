namespace api.Models._Dto.ProductosYServicios
{
    public class ProductoModeloCreateDto
    {
        public string ModeloProducto { get; set; } = string.Empty;
        public int IdMarcaProducto { get; set; } // Id de la marca del producto
        public bool Activo { get; set; } = true; // por defecto activo
    }
}