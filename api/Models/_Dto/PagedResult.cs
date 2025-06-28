namespace api.Models._Dto
{
    public class PagedResult<T>
    {
        public int TotalItems { get; set; } // total de items
        public int Page { get; set; } // pagina actual
        public int PageSize { get; set; } // cantidad de items por pagina
        public List<T> Items { get; set; } = new(); // lista con los elementos de la pagina actual
    }
}