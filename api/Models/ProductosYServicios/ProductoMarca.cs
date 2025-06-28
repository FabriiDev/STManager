using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models.ProductosYServicios
{
    [Table("producto_marca")]
    public class ProductoMarca
    {
        [Key]
        [Column("id_marca_producto")]
        public int IdMarcaProducto { get; set; }

        [Column("marca_producto")]
        [StringLength(250)]
        public string MarcaProducto { get; set; } = string.Empty;

        [Column("activo")]
        public bool Activo { get; set; } = true;

        // Relación con productos
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}