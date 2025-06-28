using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column] y [foreignkey]
using System.Collections.Generic; // para ICollection

namespace api.Models.ProductosYServicios
{
    [Table("producto_imagen")]
    public class ProductoImagen
    {
        [Key]
        [Column("id_imagen")]
        public int IdImagen { get; set; }

        [StringLength(500)] // tamanio del varchar en la base de datos
        [Column("url_imagen")]
        public string UrlImagen { get; set; } = string.Empty;

    }
}