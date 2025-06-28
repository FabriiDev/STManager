using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection
using api.Models.Principales; // para las entidad Cliente y Orden


namespace api.Models.Equipos
{
    [Table("equipo")]
    public class Equipo
    {
        [Key]
        [Column("id_equipo")]
        public int IdEquipo { get; set; } 

        [Column("numero_serie")]
        [StringLength(250)]
        public string NumeroSerie { get; set; } = string.Empty;

        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Column("id_tipo_equipo")]
        public int IdTipoEquipo { get; set; }

        [Column("id_modelo_equipo")]
        public int IdModeloEquipo { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;

        // ======================================================================
        // --- Propiedades de Navegación de Referencia (lado "muchos") ---
        // Estas son las que acceder al objeto completo de Cliente, Tipo, Marca, Modelo
        // sin necesidad de hacer JOINs manuales.
        // ======================================================================

        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        [ForeignKey("IdTipoEquipo")]
        public EquipoTipo? EquipoTipo { get; set; }

        [ForeignKey("IdMarcaEquipo")]
        public EquipoMarca? EquipoMarca { get; set; }
        
        [ForeignKey("IdModeloEquipo")]
        public EquipoModelo? EquipoModelo { get; set; }

        public ICollection<Orden>? Ordenes { get; set; } // Relacion uno a muchos con la entidad Orden

    }
}