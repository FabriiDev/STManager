using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection
using System; // para DateTime
using api.Models.Principales; // para la entidad Usuario

namespace api.Models.Finanzas
{
    [Table("caja")]
    public class Caja
    {
        [Key]
        [Column("id_caja")]
        public int IdCaja { get; set; }

        [Column("fecha_apertura")]
        public DateTime FechaApertura { get; set; } = DateTime.Now;

        [Column("fecha_cierre")]
        public DateTime? FechaCierre { get; set; } // Puede ser nulo si la caja está abierta

        [Column("total_ventas")]
        public decimal TotalVentas { get; set; } = 0.0m;

        [Column("total_ordenes")]
        public decimal TotalOrdenes { get; set; } = 0.0m;

        //totak diua t falta linkear con venta y gasto y orden? 

        [Column("observaciones")]
        [StringLength(500)] // puede ser null
        public string? Observaciones { get; set; } = string.Empty;

        [Column("id_usuario_apertura")]
        public int IdUsuarioApertura { get; set; }

        [Column("id_usuario_cierre")]
        public int? IdUsuarioCierre { get; set; } // Puede ser nulo si la caja aún está abierta

        [ForeignKey("IdUsuarioApertura")]
        public Usuario? UsuarioApertura { get; set; } // Relacion con la entidad Usuario para el usuario que abrió la caja

        [ForeignKey("IdUsuarioCierre")]
        public Usuario? UsuarioCierre { get; set; } // Relacion con la entidad Usuario para el usuario que cerró la caja

    }
}
