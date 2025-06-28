namespace api.Models._Dto.Principales
{
    public class OrdenCreateDto
    {
        public int IdEquipo { get; set; } // ID del equipo, no puede ser nulo
        public int IdCliente { get; set; } // ID del cliente, no puede ser nulo
        public int IdUsuarioCreador { get; set; } // ID del usuario creador, no puede ser nulo
        public int? IdUsuarioEntrega { get; set; } // ID del usuario que entrega, puede ser nulo si aún no se ha entregado
        public int IdUsuarioAsignado { get; set; } // ID del usuario asignado, no puede ser nulo
        public string Falla { get; set; } = string.Empty; // Descripción de la falla, no puede ser nula
        public string? DetalleTecnico { get; set; } = string.Empty; // Detalle técnico opcional
        public DateTime FechaIngreso { get; set; } = DateTime.UtcNow; // Fecha de ingreso, por defecto es la hora global UTC
        public DateTime? FechaEntrega { get; set; } // Fecha de entrega, puede ser nula si aún no se ha entregado
        public DateTime? FechaPresupuesto { get; set; } // Fecha de entrega, puede ser nula si aún no se ha entregado
        public bool Cargador { get; set; } = false; // Indica si incluye cargador, por defecto es false
        public string? Extras { get; set; } = string.Empty; // Extras opcionales
        public string? Diagnostico { get; set; } = string.Empty; // Diagnóstico opcional
        public decimal Presupuesto { get; set; } = 0.0m; // Presupuesto, por defecto es 0.0
        public bool PagoDiagnostico { get; set; } = false; // Indica si se ha pagado el diagnóstico, por defecto es false
        public bool Entregada { get; set; } = false; // no entregada por defecto
        public decimal Total { get; set; } = 0.0m; // Total de la orden, por defecto es 0.0
        public bool Activo { get; set; } = true; // Indica si la orden está activa, por defecto es true
    }
}