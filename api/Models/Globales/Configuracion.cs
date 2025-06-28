using System.ComponentModel.DataAnnotations; // para los [key] y [stringlength]
using System.ComponentModel.DataAnnotations.Schema; // para los [table] y [column]
using System.Collections.Generic; // para ICollection

namespace api.Models.Globales
{
    [Table("configuracion")]
    public class Configuracion
    {

        [Key]
        [Column("id_configuracion")]
        public int IdConfiguracion { get; set; }

        [Column("nombre_taller")]
        [StringLength(250)]
        public string NombreTaller { get; set; } = string.Empty;

        [Column("direccion_taller")]
        [StringLength(200)]
        public string DireccionTaller { get; set; } = string.Empty;

        [Column("valor_diagnostico")]
        public decimal ValorDiagnostico { get; set; } = 0.0m;

        [Column("url_logo")]
        [StringLength(500)] // puede ser null
        public string? UrlLogo { get; set; } = string.Empty;

        [Column("telefono_taller")]
        [StringLength(250)]
        public string TelefonoTaller { get; set; } = string.Empty;

        [Column("eslogan")]
        [StringLength(250)] // puede ser null
        public string? Eslogan { get; set; } = string.Empty;

        [Column("texto_pie")]
        [StringLength(500)] // puede ser null
        public string? TextoPie { get; set; } = string.Empty;

        [Column("texto_legal")]
        [StringLength(500)] // puede ser null
        public string? TextoLegal { get; set; } = string.Empty;
        
        // Otros campos de configuración según sea necesario
    }
}