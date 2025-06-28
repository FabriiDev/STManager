namespace api.Models._Dto
{
    public class PaginationDto
    {
        public int PageNumber { get; set; } = 1; // por defecto la primera pagina
        public int PageSize { get; set; } = 1; // por defecto 10 registros por pagina
    }
}