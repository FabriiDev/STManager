namespace api.Models._Dto.Equipos{
    public class EquipoModeloCreateDto
    {
        public string Modelo { get; set; } = string.Empty; // nombre del modelo, no puede ser nulo
        public int IdMarcaEquipo { get; set; } // id de la marca, no puede ser nulo
        public bool Activo { get; set; } = true; // indica si el modelo está activo, por defecto es true
    }
}