namespace api.Models._Dto.Relaciones
{
    public class DetalleOrdenProductoDto
    {
        public int NroOrden { get; set; } // Número de la orden a la que pertenece el detalle
        public int IdProducto { get; set; } // ID del producto asociado al detalle
        public int Cantidad { get; set; } = 1; // Cantidad del producto en el detalle, por defecto es 1
        public decimal PrecioUnitario { get; set; } = 0.0m; // Precio unitario del producto, por defecto es 0.0
    }
}