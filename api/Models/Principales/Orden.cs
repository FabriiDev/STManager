using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection
using System; // para DateTime
using api.Models.Relaciones; // para los detalle
using api.Models.Principales; // para las entidades Cliente y Usuario
using api.Models.Equipos; // para la entidad Equipo


namespace api.Models.Principales
{
    [Table("orden")]
    public class Orden
    {
        [Key]
        [Column("nro_orden")]
        public int NroOrden { get; set; }

        [Column("id_equipo")]
        public int IdEquipo { get; set; }

        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Column("id_usuario_creador")]
        public int IdUsuarioCreador { get; set; }

        [Column("id_usuario_entrega")]
        public int? IdUsuarioEntrega { get; set; } // Puede ser nulo si aún no se ha entregado

        [Column("id_usuario_asignado")]
        public int IdUsuarioAsignado { get; set; }

        [Column("falla")]
        [StringLength(500)]
        public string Falla { get; set; } = string.Empty;

        [Column("detalle_tecnico")] // puede ser null
        [StringLength(500)]
        public string? DetalleTecnico { get; set; } = string.Empty;

        [Column("fecha_ingreso")]
        public DateTime FechaIngreso { get; set; } = DateTime.UtcNow; // hora global UTC por defecto -> por si el server esta alojado en otro pais

        [Column("fecha_entrega")]
        public DateTime? FechaEntrega { get; set; } // Puede ser nulo si aún no se ha entregado

        [Column("fecha_presupuesto")]
        public DateTime? FechaPresupuesto { get; set; } // Puede ser nulo si aún no se ha presupuestado

        [Column("cargador")]
        public bool Cargador { get; set; } = false;

        [Column("extras")]
        [StringLength(500)] // Puede ser null
        public string? Extras { get; set; } = string.Empty;

        [Column("diagnostico")] // Puede ser null si aun no se ha realizado
        [StringLength(500)]
        public string? Diagnostico { get; set; } = string.Empty;

        [Column("presupuesto")]
        public decimal Presupuesto { get; set; } = 0.0m; 

        [Column("pago_diagnostico")]
        public bool PagoDiagnostico { get; set; } = false;

        [Column("estado")]
        [StringLength(50)]
        public string Estado { get; set; } = "No_revisada";

        [Column("entregada")]
        public bool Entregada { get; set; } = false;

        [Column("total")]
        public decimal Total { get; set; } = 0.0m;

        [Column("activo")]
        public bool Activo { get; set; } = true;

        [ForeignKey("IdEquipo")]
        public Equipo? Equipo { get; set; } // Relación con la entidad Equipo

        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; } // Relación con la entidad Cliente

        [ForeignKey("IdUsuarioCreador")]
        public Usuario? UsuarioCreador { get; set; } // Relación con la entidad Usuario

        [ForeignKey("IdUsuarioEntrega")]
        public Usuario? UsuarioEntrega { get; set; } // Relación con la entidad Usuario (entrega)

        [ForeignKey("IdUsuarioAsignado")]
        public Usuario? UsuarioAsignado { get; set; } // Relación con la entidad Usuario (asignado)

        // =====================================

        public ICollection<DetalleOrdenServicio>? DetalleOrdenServicios { get; set; } // Relación uno a muchos con la entidad DetalleOrdenServicio

        public ICollection<DetalleOrdenProducto>? DetallesOrdenProducto { get; set; } // Relacion uno a muchos con la entidad DetalleOrdenProducto
        
    }
}