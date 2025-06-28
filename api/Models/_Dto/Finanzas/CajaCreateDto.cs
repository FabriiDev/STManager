using System;

namespace api.Models._Dto.Finanzas
{
    public class CajaCreateDto
    {
        public DateTime FechaApertura { get; set; } = DateTime.UtcNow; // Fecha de apertura, por defecto es la fecha actual en UTC
        public DateTime? FechaCierre { get; set; } = null; // Fecha de cierre, puede ser nula si la caja está abierta
        public decimal TotalVentas { get; set; } = 0.0m; // Total de ventas, por defecto es 0.0
        public decimal TotalOrdenes { get; set; } = 0.0m; // Total de órdenes, por defecto es 0.0
        public string? Observaciones { get; set; } = string.Empty; // Observaciones, puede ser nulo
        public int IdUsuarioApertura { get; set; } // ID del usuario que abre la caja, no puede ser nulo
        public int? IdUsuarioCierre { get; set; } = null; // ID del usuario que cierra la caja, puede ser nulo si la caja aún está abierta
    }
}
