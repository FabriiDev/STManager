using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models
{
    [Table("stmanager_db_cliente")]
    // esto hace referencia a la tabla cliente en la db con ef
    public class Cliente
    {
        [Key]
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [StringLength(250)] // tamanio del varchar en la base de datos
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("apellido")]
        [StringLength(250)]
        public string Apellido { get; set; } = string.Empty;

        
        [Column("direccion")]
        [StringLength(250)]
        public string Direccion { get; set; } = string.Empty;

        [Column("celular")]
        [StringLength(250)]
        public string Celular { get; set; } = string.Empty;

        [Column("otros")]
        [StringLength(250)]
        public string Otros { get; set; } = string.Empty;

        [Column("activo")]
        public bool Activo { get; set; } = true;

        public ICollection<Equipo>? Equipos { get; set; } // Relacion uno a muchos con la entidad Equipo
        public ICollection<Orden>? Ordenes { get; set; } // Relacion uno a muchos con la entidad Orden
        
        }

}