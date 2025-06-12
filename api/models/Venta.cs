using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column] y [foreignkey]
using System.Collections.Generic; // para ICollection
using System; // para DateTime

namespace api.Models
{
    [Table("stmanager_db_venta")]
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

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; } // Relacion con la entidad Usuario
        
        public ICollection<DetalleVentaProducto>? DetallesVentaProducto { get; set; } // Relacion uno a muchos con la entidad DetalleVentaProducto

    }
}