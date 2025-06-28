using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column] y [foreignkey]
using System.Collections.Generic; // para ICollection
using System; // para DateTime
using api.Models.Relaciones; // para los detalle
using api.Models.Principales; // para usuario
using api.Models.Finanzas; // para metodopago y caja

namespace api.Models.Finanzas
{
    [Table("venta")]
    public class Venta
    {
        [Key]
        [Column("id_venta")]
        public int IdVenta { get; set; }

        [Column("fecha_venta")]
        public DateTime FechaVenta { get; set; } = DateTime.Now;

        [Column("total")]
        public decimal Total { get; set; } = 0.0m;

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("id_caja")]
        public int IdCaja { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; } // Relacion con la entidad Usuario

        [ForeignKey("IdCaja")]
        public Caja? Caja { get; set; } // Relacion con la entidad Caja

        [Column("estado")]
        [StringLength(50)] // tamanio del varchar en la base de datos
        public bool Estado { get; set; } = false; // Por defecto, el estado es 0 false "Pendiente"

        [Column("id_metodo_pago")]
        public int IdMetodoPago { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true; // Por defecto, la venta está activa

        [ForeignKey("IdMetodoPago")]
        public MetodoPago? MetodoPago { get; set; } // Relacion con la entidad MetodoPago
        
        public ICollection<DetalleVentaProducto>? DetallesVentaProducto { get; set; } // Relacion uno a muchos con la entidad DetalleVentaProducto

    }
}