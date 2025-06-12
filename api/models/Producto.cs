using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models
{
    [Table("stmanager_db_producto")]
    public class Producto
    {
        [Key]
        [Column("id_producto")]
        public int IdProducto { get; set; }

        [StringLength(250)] // tamanio del varchar en la base de datos
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("descripcion")]
        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Column("precio")]
        public decimal Precio { get; set; } = 0.0m;

        [Column("stock")]
        public int Stock { get; set; };

        [Column("activo")]
        public bool Activo { get; set; } = true;

        public ICollection<DetalleOrdenProducto>? DetallesOrdenProducto { get; set; } // Relacion uno a muchos con la entidad DetalleOrdenProducto

        public ICollection<DetalleVentaProducto>? DetallesVentaProducto { get; set; } // Relacion uno a muchos con la entidad DetalleVentaProducto

    }
}