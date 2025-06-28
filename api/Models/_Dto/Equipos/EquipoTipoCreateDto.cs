namespace api.Models._Dto.Equipos
{
    public class EquipoTipoCreateDto
    {
        public string Tipo { get; set; } = string.Empty; // caracteristica del tipo de equipo, no puede ser nulo
        public bool Activo { get; set; } = true; // indica si el tipo de equipo está activo, por defecto es true
    }
}