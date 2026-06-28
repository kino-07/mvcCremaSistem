using Microsoft.EntityFrameworkCore;
using mvcCremaSistem.Models;

namespace mvcCremaSistem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        // Aqui todos los modelos que se creen
         public DbSet<Usuario> Usuario { get; set; }
         public DbSet<Producto> Producto { get; set; } = default!;
         public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<DetallePedidos> DetallePedidos { get; set; }
        public DbSet<Gramajes> Gramajes { get; set; }
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<ActividadUsuario> ActividadUsuario { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<mvcCremaSistem.Models.Configuracion> Configuracion { get; set; } = default!;
        public DbSet<mvcCremaSistem.Models.PedidosCompleto> PedidosCompleto { get; set; } = default!;
        public DbSet<mvcCremaSistem.Models.ProductosCompleto> ProductosCompleto { get; set; } = default!;
        public DbSet<mvcCremaSistem.Models.MenuProductoViewModel> MenuProductoViewModel { get; set; } = default!;
    }
}
