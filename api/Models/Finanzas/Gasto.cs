using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column] y [foreignkey]
using System.Collections.Generic; // para ICollection
using api.Models.Principales; // para la entidad Usuario
using System; // para DateTime
using api.Models.Finanzas; // para MetodoPago y Caja

namespace api.Models.Finanzas
{
    [Table("gasto")]
    public class Gasto
    {
        [Key]
        [Column("id_gasto")]
        public int IdGasto { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("id_caja")]
        public int IdCaja { get; set; }

        [Column("id_metodo_pago")]
        public int IdMetodoPago { get; set; }

        [Column("descripcion")]
        [StringLength(250)] // tamanio del varchar en la base de datos
        public string Descripcion { get; set; } = string.Empty;

        [Column("monto")]
        public decimal Monto { get; set; } = 0.0m;

        [Column("fecha_gasto")]
        public DateTime FechaGasto { get; set; } = DateTime.Now;

        [Column("estado")]
        public bool Estado { get; set; } = false;

        [Column("activo")]
        public bool Activo { get; set; } = true;

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; } // Relacion con la entidad Usuario

        [ForeignKey("IdCaja")]
        public Caja? Caja { get; set; } // Relacion con la entidad Caja

        [ForeignKey("IdMetodoPago")]
        public MetodoPago? MetodoPago { get; set; } // Relacion con la entidad MetodoPago

        
    }
}