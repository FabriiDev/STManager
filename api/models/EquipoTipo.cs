using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models
{
    [Table("stmanager_db_equipo_tipo")]
    public class EquipoTipo
    {
        [Key]
        [Column("id_tipo_equipo")]
        public int IdTipoEquipo { get; set; }

        [Column("tipo")]
        [StringLength(250)]
        public string Tipo { get; set; } = string.Empty;

        [Column("activo")]
        public bool Activo { get; set; } = true;

        // Relación con los equipos
        public ICollection<Equipo>? Equipos { get; set; }
    }
}