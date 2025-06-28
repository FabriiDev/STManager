namespace api.Models._Dto.Equipos
{
    public class EquipoCreateDto
    {
        public string NumeroSerie { get; set; } = string.Empty;
        public int IdCliente { get; set; }
        public int IdTipoEquipo { get; set; }
        public int IdModeloEquipo { get; set; }
        public bool Activo { get; set; } = true;
    }
}