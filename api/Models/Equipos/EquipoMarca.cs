using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models.Equipos
{
    [Table("equipo_marca")]
    public class EquipoMarca
    {
        [Key]
        [Column("id_marca_equipo")]
        public int IdMarcaEquipo { get; set; }

        [Column("marca")]
        [StringLength(250)]
        public string Marca { get; set; } = string.Empty;

        [Column("activo")]
        public bool Activo { get; set; } = true;

        // Relación con los modelos de equipo
        public ICollection<EquipoModelo>? EquipoModelos { get; set; }
    }
}