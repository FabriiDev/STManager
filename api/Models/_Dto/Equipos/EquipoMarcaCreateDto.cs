namespace api.Models._Dto.Equipos{
    
    public class EquipoMarcaCreateDto
    {
        public string Marca { get; set; } = string.Empty; // nombre de la marca, no puede ser nulo
        public bool Activo { get; set; } = true; // indica si la marca está activa, por defecto es true

    }
}