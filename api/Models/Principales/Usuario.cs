using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models.Principales
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [StringLength(250)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [StringLength(250)]
        [Column("nick")]
        public string Nick { get; set; } = string.Empty;

        [Column("password")]
        [StringLength(250)]
        public string Password { get; set; } = string.Empty;

        [Column("rol")]
        public string Rol { get; set; } = string.Empty;

        [Column("activo")]
        public bool Activo { get; set; } = true;

        ICollection<Orden>? OrdenesCreadas { get; set; } // Relacion uno a muchos con la entidad Orden
        ICollection<Orden>? OrdenesEntregadas { get; set; } // Relacion uno a muchos con la entidad Orden
        ICollection<Orden>? OrdenesAsignadas { get; set; } // Relacion uno a muchos con la entidad Orden

    }
}