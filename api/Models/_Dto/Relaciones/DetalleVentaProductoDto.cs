namespace api.Models._Dto.Relaciones
{
    public class DetalleVentaProductoDto
    {
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; } = 0.0m;
    }
}