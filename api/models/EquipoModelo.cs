using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models
{
    [Table("stmanager_db_equipo_modelo")]
    public class EquipoModelo
    {
        [Key]
        [Column("id_modelo_equipo")]
        public int IdModeloEquipo { get; set; }

        [Column("modelo")]
        [StringLength(250)]
        public string Modelo { get; set; } = string.Empty;

        [Column("id_marca_equipo")]
        public int IdMarcaEquipo { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;

        // Relación con los equipos
        public ICollection<Equipo>? Equipos { get; set; }
        // Relación con la marca del equipo
        [ForeignKey("IdMarcaEquipo")]
        public EquipoMarca? EquipoMarca { get; set; }
    }
}