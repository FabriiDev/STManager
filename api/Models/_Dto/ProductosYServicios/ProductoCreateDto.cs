namespace api.Models._Dto.ProductosYServicios
{
    public class ProductoCreateDto
    {
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; } = true; 
        
        public int IdMarcaProducto {get; set;}
        public int IdModeloProducto {get; set;}

        public int IdTipoProducto { get; set; } 
        public int? idImagenProducto {get; set;}
        //public IFormFile? Imagen { get; set; } // IFormFile para recibir archivos en el controlador
    }
}