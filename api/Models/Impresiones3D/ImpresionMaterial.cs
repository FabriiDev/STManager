using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models.Impresiones3D
{
    [Table("impresion_material")]
    public class ImpresionMaterial
    {
        [Key]
        [Column("id_material")]
        public int IdMaterial { get; set; }

        [Column("nombre")]
        [StringLength(250)]
        public string Nombre { get; set; } = string.Empty;

        [Column("tipo_material")]
        [StringLength(100)]
        public string TipoMaterial { get; set; } = string.Empty;

        [Column("color")]
        [StringLength(50)]
        public string Color { get; set; } = string.Empty;

        [Column("descripcion")]
        [StringLength(500)]
        public string? Descripcion { get; set; } = string.Empty;

        [Column("activo")]
        public bool Activo { get; set; } = true;

        public ICollection<ImpresionItem> Items { get; set; } = new List<ImpresionItem>();
    }
}