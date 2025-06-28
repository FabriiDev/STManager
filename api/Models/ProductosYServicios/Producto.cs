using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection
using api.Models.Relaciones; // para las entidades DetalleOrdenProducto y DetalleVentaProducto

namespace api.Models.ProductosYServicios
{
    [Table("producto")]
    public class Producto
    {
        [Key]
        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Column("descripcion")]
        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Column("precio")]
        public decimal Precio { get; set; } = 0.0m;

        [Column("stock")]
        public int Stock { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;

        [Column("id_tipo_producto")]
        public int IdTipoProducto { get; set; } // Llave foranea a ProductoTipo

        [Column("id_imagen_producto")]
        public int? IdImagenProducto { get; set; } // Llave foranea a ProductoImagen, puede ser nula si no tiene imagen

        [Column("id_marca_producto")]
        public int IdMarcaProducto { get; set; } 

        [Column("id_modelo_producto")]
        public int IdModeloProducto { get; set; } // Llave foranea a ProductoModelo, puede ser nula si no tiene modelo

        [ForeignKey("IdTipoProducto")]
        public ProductoTipo? TipoProducto { get; set; } // Relacion muchos a uno con la entidad ProductoTipo

        [ForeignKey("IdImagenProducto")]
        public ProductoImagen? ImagenProducto { get; set; } // Relacion uno a uno con la entidad ProductoImagen

        [ForeignKey("IdMarcaProducto")]
        public ProductoMarca MarcaProducto { get; set; }

        [ForeignKey("IdModeloProducto")]
        public ProductoModelo? ModeloProducto { get; set; } 



        public ICollection<DetalleOrdenProducto>? DetallesOrdenProducto { get; set; } // Relacion uno a muchos con la entidad DetalleOrdenProducto

        public ICollection<DetalleVentaProducto>? DetallesVentaProducto { get; set; } // Relacion uno a muchos con la entidad DetalleVentaProducto

    }
}