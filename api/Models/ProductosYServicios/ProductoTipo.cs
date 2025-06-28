using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column] y [foreignkey]
using System.Collections.Generic; // para ICollection

namespace api.Models.ProductosYServicios
{
    [Table("producto_tipo")]
    public class ProductoTipo
    {
        [Key]
        [Column("id_tipo_producto")]
        public int IdTipoProducto { get; set; }

        [StringLength(250)] // tamanio del varchar en la base de datos
        [Column("tipo_producto")]
        public string TipoProducto { get; set; } = string.Empty;

        [Column("activo")]
        public bool Activo { get; set; } = true;

        public ICollection<Producto>? Productos { get; set; } // Relacion uno a muchos con la entidad Producto
    }
}