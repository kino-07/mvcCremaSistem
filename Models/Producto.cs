using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;

namespace mvcCremaSistem.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string ProductoNombre { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }
        public Categorias? Categoria { get; set; }

        [StringLength(500)]
        public string? Descripcion { get; set; }

        public List<Gramajes> Gramajes { get; set; } = new();

        [Column(TypeName = "nvarchar(MAX)")]
        public string? Imagen { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = "Activo";

        public bool Destacado { get; set; } = false;

        [NotMapped]
        public decimal PrecioMin => Gramajes.Any() ? Gramajes.Min(g => g.Precio) : 0;

        [NotMapped]
        public decimal PrecioMax => Gramajes.Any() ? Gramajes.Max(g => g.Precio) : 0;

        [NotMapped]
        [Display(Name = "Foto del Producto")]
        public IFormFile? ImagenArchivo { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }
}