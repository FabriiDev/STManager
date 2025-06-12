using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models
{
    [Table("stmanager_db_detalle_venta_producto")]
    public class DetalleVentaProducto
    {
        [Key]
        [Column("id_detalle")]
        public int IdDetalleVentaProducto { get; set; }

        [Column("id_venta")]
        public int IdVenta { get; set; }

        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; };

        [Column("precio_unitario")]
        public decimal PrecioUnitario { get; set; } = 0.0m;

        [ForeignKey("IdVenta")]
        public Venta? Venta { get; set; } // Relacion con la entidad Venta

        [ForeignKey("IdProducto")]
        public Producto? Producto { get; set; } // Relacion con la entidad Producto
    }
}