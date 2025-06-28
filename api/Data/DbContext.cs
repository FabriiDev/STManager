using Microsoft.EntityFrameworkCore;
// modelos
using api.Models.Finanzas;
using api.Models.Equipos;
using api.Models.Globales;
using api.Models.Impresiones3D;
using api.Models.Principales;
using api.Models.ProductosYServicios;
using api.Models.Relaciones;
// fin modelos

namespace api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet para las entidades (tablas)
        // ======================================================================

        // entidades principales
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Configuracion> Configuraciones { get; set; }

        // EQUIPO
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<EquipoMarca> EquipoMarcas { get; set; }
        public DbSet<EquipoModelo> EquipoModelos { get; set; }
        public DbSet<EquipoTipo> EquipoTipos { get; set; }

        // control de ingresos y egresos
        public DbSet<Venta> Ventas { get; set; }     
        public DbSet<Caja> Cajas { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }

        // productos
        public DbSet<Producto> Productos { get; set; } 
        public DbSet<ProductoTipo> ProductoTipos { get; set; }
        public DbSet<ProductoMarca> ProductoMarcas { get; set; }
        public DbSet<ProductoModelo> ProductoModelos { get; set; }
        public DbSet<ProductoImagen> ProductoImagenes { get; set; }

        // impresiones 3D
        public DbSet<Impresion3D> Impresiones3D { get; set; }
        public DbSet<ImpresionItem> ImpresionItems { get; set; }
        public DbSet<ImpresionMaterial> ImpresionMateriales { get; set; }

        // muchos a muchos
        public DbSet<DetalleOrdenServicio> DetalleOrdenServicios { get; set; }
        public DbSet<DetalleOrdenProducto> DetalleOrdenProductos { get; set; } 
        public DbSet<DetalleVentaProducto> DetalleVentaProductos { get; set; } 

           

        // ======================================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // aca por el momento no hago nada, no necesito configuraciones avanzadas
            // por que tengo todo mapeado desde los modelos
        }
    }
}