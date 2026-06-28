using System.Collections.Generic;

namespace mvcCremaSistem.Models
{
    // ViewModel para mostrar cada producto en el menú
    public class MenuProductoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }
        public decimal Precio { get; set; }
        public string Gramaje { get; set; } = string.Empty; // Ej: "30g"
        public string Estado { get; set; } = string.Empty; // Ej: "Clásico", "Premium"
        public bool Destacado { get; set; }
    }

    // Modelo para la página del menú
    public class MenuProductos
    {
        public List<Producto> Producto { get; set; } = new();
    }
}
