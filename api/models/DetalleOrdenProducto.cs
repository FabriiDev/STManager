using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models
{
    [Table("stmanager_db_detalle_orden_producto")]
    public class DetalleOrdenProducto
    {
        [Key]
        [Column("id_detalle")]
        public int IdDetalleOrdenProducto { get; set; }

        [Column("nro_orden")]
        public int NroOrden { get; set; }

        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; } = 1;

        [Column("precio_unitario")]
        public decimal PrecioUnitario { get; set; } = 0.0m;

        [ForeignKey("NroOrden")]
        public Orden? Orden { get; set; } // Relacion con la entidad Orden

        [ForeignKey("IdProducto")]
        public Producto? Producto { get; set; } // Relacion con la entidad Producto
    }
}