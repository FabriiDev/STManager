namespace api.Models._Dto.Finanzas
{
    public class MetodoPagoCreateDto
    {
        public string Metodo { get; set; } = string.Empty; // Nombre del método de pago, no puede ser nulo
    }
}