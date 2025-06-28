using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection
using System; // para DateTime
using api.Models.Principales; // para Cliente
using api.Models.Finanzas; // para Venta


namespace api.Models.Impresiones3D
{
    [Table("impresion_3d")]
    public class Impresion3D
    {
        [Key]
        [Column("id_impresion_3d")]
        public int IdImpresion3D { get; set; }

        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Column("id_venta")]
        public int? IdVenta { get; set; } // Puede ser nulo si no está asociado a una venta

        [Column("fecha_ingreso")]
        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        [Column("fecha_estimada_entrega")]
        public DateTime FechaEstimadaEntrega { get; set; } = DateTime.Now.AddDays(7);

        [Column("fecha_entrega")]
        public DateTime? FechaEntrega { get; set; } // Puede ser nulo si no se ha entregado

        [Column("descripcion")]
        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Column("total")]
        public decimal Total { get; set; } = 0.0m;

        [Column("estado")]
        [StringLength(50)]
        public string Estado { get; set; } = "Pendiente"; // Estado por defecto

        [Column("entregada")]
        public bool Entregada { get; set; } = false; // Por defecto, no está entregada

        [Column("activo")]
        public bool Activo { get; set; } = true;

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; } // Relacion con la entidad Cliente

        [ForeignKey("IdVenta")]
        public Venta? Venta { get; set; } // Relacion con la entidad Venta, puede ser nula

        public ICollection<ImpresionItem> Items { get; set; } = new List<ImpresionItem>();

    }
}