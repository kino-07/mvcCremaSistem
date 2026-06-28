using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mvcCremaSistem.Models
{
    public class Carrito
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [Required]
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }

        [ForeignKey("Gramaje")]
        public int? GramajeId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        public DateTime FechaAgregado { get; set; }
        public DateTime FechaActualizacion { get; set; }

        // Relaciones de navegación
        public Usuario? Usuario { get; set; }
        public Producto? Producto { get; set; }
        public Gramajes? Gramaje { get; set; }
    }
}