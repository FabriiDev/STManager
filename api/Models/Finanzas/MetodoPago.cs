using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column] y [foreignkey]
using System.Collections.Generic; // para ICollection

namespace api.Models.Finanzas
{
    [Table("metodo_pago")]
    public class MetodoPago
    {
        [Key]
        [Column("id_metodo_pago")]
        public int IdMetodoPago { get; set; }

        [StringLength(250)] // tamanio del varchar en la base de datos
        [Column("metodo")]
        public string Metodo { get; set; } = string.Empty;

        
    }
}