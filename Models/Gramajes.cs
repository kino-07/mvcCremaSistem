using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mvcCremaSistem.Models
{
    public class Gramajes
    {
        [Key]
        public int Id { get; set; }

        // AGREGADO: faltaba la FK que conecta el gramaje con su producto.
        // Sin esto, EF Core no podía resolver correctamente la relación
        // 1 Producto -> N Gramajes que se usa en Producto.cs (List<Gramajes>).
        [Required]
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }

        [Required]
        public int Gramos { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Relación de navegación (ahora correctamente respaldada por ProductoId)
        public Producto? Producto { get; set; }
    }
}
