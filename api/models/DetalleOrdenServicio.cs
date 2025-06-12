using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models
{
    [Table("stmanager_db_detalle_orden_servicio")]
    public class DetalleOrdenServicio
    {
        [Key]
        [Column("id_detalle")]
        public int IdDetalleOrdenServicio { get; set; }

        [Column("id_orden")]
        public int IdOrden { get; set; }

        [Column("id_servicio")]
        public int IdServicio { get; set; }

        [Column("precio")]
        public decimal PrecioUnitario { get; set; } = 0;

        // Relacion con Orden
        [ForeignKey("IdOrden")]
        public Orden? Orden { get; set; }

        // Relacion con Servicio
        [ForeignKey("IdServicio")]
        public Servicio? Servicio { get; set; }
    }
}