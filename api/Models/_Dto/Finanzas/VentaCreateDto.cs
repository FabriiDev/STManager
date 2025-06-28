using System;

namespace api.Models._Dto.Finanzas
{
    public class VentaCreateDto
    {
        public DateTime FechaVenta { get; set; } = DateTime.Now; // Fecha de la venta, por defecto es la fecha actual
        public decimal Total { get; set; } = 0.0m; // Total de la venta, por defecto es 0.0
        public int IdUsuario { get; set; } // ID del usuario que realiza la venta, no puede ser nulo
        public int IdCaja { get; set; } // ID de la caja donde se realiza la venta, no puede ser nulo
        public bool Estado { get; set; } = false; // Estado de la venta, por defecto es false (Pendiente)
        public int IdMetodoPago { get; set; } // ID del método de pago utilizado, no puede ser nulo
        public bool Activo { get; set; } = true; // Indica si la venta está activa, por defecto es true

    }
}