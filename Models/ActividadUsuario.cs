using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mvcCremaSistem.Models
{
    public class ActividadUsuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(20)]
        public string Tipo { get; set; }

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [StringLength(45)]
        public string? IP { get; set; }

        public DateTime Fecha { get; set; }

        // Relación de navegación
        public Usuario? Usuario { get; set; }
    }
}