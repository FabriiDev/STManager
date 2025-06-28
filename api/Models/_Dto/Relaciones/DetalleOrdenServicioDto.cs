namespace api.Models._Dto.Relaciones
{
    public class DetalleOrdenServicioDto
    {
        public int IdOrden { get; set; } // ID de la orden, no puede ser nulo
        public int IdServicio { get; set; } // ID del servicio, no puede ser nulo
        public decimal Precio { get; set; } = 0.0m; // Precio unitario del servicio, por defecto es 0.0
    }
}