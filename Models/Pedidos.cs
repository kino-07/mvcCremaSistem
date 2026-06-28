using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mvcCremaSistem.Models
{
    public class Pedidos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Codigo { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(100)]
        public string UsuarioNombre { get; set; }

        [StringLength(100)]
        public string? UsuarioEmail { get; set; }

        [StringLength(20)]
        public string? UsuarioTelefono { get; set; }

        public DateTime FechaPedido { get; set; }
        public DateTime? FechaEntrega { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        [StringLength(30)]
        public string MetodoPago { get; set; }

        [StringLength(20)]
        public string EstadoPago { get; set; }

        public DateTime? FechaPago { get; set; }
        public string? InstruccionesEspeciales { get; set; }
        public string? Notas { get; set; }
        public DateTime FechaActualizacion { get; set; }

        // Relación de navegación
        public Usuario? Usuario { get; set; }
    }
}