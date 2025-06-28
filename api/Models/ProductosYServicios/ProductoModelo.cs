using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models.ProductosYServicios
{
    [Table("producto_modelo")]
    public class ProductoModelo
    {
        [Key]
        [Column("id_modelo_producto")]
        public int IdModeloProducto { get; set; }

        [Column("modelo_producto")]
        [StringLength(250)]
        public string ModeloProducto { get; set; } = string.Empty;

        [Column("id_marca_producto")]
        public int IdMarcaProducto { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;

        [ForeignKey("IdMarcaProducto")]
        public ProductoMarca? MarcaProducto { get; set; } // Relación con la entidad ProductoMarca

        // Relación con productos
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}