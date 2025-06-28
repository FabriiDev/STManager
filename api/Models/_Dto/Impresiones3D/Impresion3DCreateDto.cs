using System; 

namespace api.Models._Dto.Impresiones3D
{
    public class Impresion3DCreateDto
    {
        public int IdCliente { get; set; } // ID del cliente, no puede ser nulo
        public int? IdVenta { get; set; } // ID de la venta, puede ser nulo si no está asociado a una venta
        public DateTime FechaIngreso { get; set; } = DateTime.UtcNow; // Fecha de ingreso, por defecto es la fecha actual utc now para hora global
        public DateTime FechaEstimadaEntrega { get; set; } = DateTime.UtcNow; // Fecha estimada de entrega
        public DateTime? FechaEntrega { get; set; } = null; // Fecha de entrega, puede ser nula si no se ha entregado
        public string Descripcion { get; set; } = string.Empty; // Descripción de la impresión, no puede ser nula
        public decimal Total { get; set; } = 0.0m; // Total de la impresión, por defecto es 0.0
        public string Estado { get; set; } = "Pendiente"; // Estado de la impresión, por defecto es "Pendiente"
        public bool Entregada { get; set; } = false; // Indica si la impresión ha sido entregada, por defecto es false
        public bool Activo { get; set; } = true; // Indica si la impresión está activa, por defecto es true
    }
}