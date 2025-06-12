using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models
{
    [Table("stmanager_db_servicio")]
    public class Servicio
    {
        [Key]
        [Column("id_servicio")]
        public int IdServicio { get; set; }

        [StringLength(250)] // tamanio del varchar en la base de datos
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("descripcion")]
        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Column("precio")]
        public decimal Precio { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;

        public ICollection<DetalleOrdenServicio>? DetallesOrdenServicio { get; set; } // Relacion uno a muchos con la entidad DetalleOrdenServicio

        
    }
}