using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mvcCremaSistem.Models
{
    public class DetallePedidos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Pedidos")]
        public int PedidoId { get; set; }

        [Required]
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductoNombre { get; set; }

        [StringLength(500)]
        public string? ProductoDescripcion { get; set; }

        // CORREGIDO: decía [ForeignKey("Gramaje")] pero la propiedad de
        // navegación se llama "Gramajes". Ahora coinciden.
        [ForeignKey("Gramajes")]
        public int? GramajeId { get; set; }

        [StringLength(20)]
        public string? GramajeSeleccionado { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }

        // Relaciones de navegación
        public Pedidos? Pedidos { get; set; }
        public Producto? Producto { get; set; }
        public Gramajes? Gramajes { get; set; }
    }
}