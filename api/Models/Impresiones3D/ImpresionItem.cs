using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models.Impresiones3D
{
    [Table("impresion_item")]
    public class ImpresionItem
    {
        [Key]
        [Column("id_impresion_item")]
        public int IdImpresionItem { get; set; }

        [Column("id_impresion_3d")]
        public int IdImpresion3D { get; set; }

        [Column("nombre")]
        [StringLength(250)]
        public string Nombre { get; set; } = string.Empty;

        [Column("descripcion")]
        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Column("cantidad")]
        public int Cantidad { get; set; } = 1;

        [Column("id_material")]
        public int IdMaterial { get; set; }

        [ForeignKey("IdMaterial")]
        public ImpresionMaterial ImpresionMaterial { get; set; } // Relacion con la entidad ImpresionMaterial

        
        [Column("precio_unitario")]
        public decimal PrecioUnitario { get; set; } = 0.0m;

        [ForeignKey("IdImpresion3D")]
        public Impresion3D Impresion3D { get; set; } // Relacion con la entidad Impresion3D
    }
}