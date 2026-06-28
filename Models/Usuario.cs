using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcCremaSistem.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        // CORREGIDO: se quitó "= string.Empty" en las propiedades "required".
        // "required" ya obliga a inicializar la propiedad al crear el objeto
        // (new Usuario { NombreUsuario = "...", ... }), por lo que el valor
        // por defecto nunca llega a usarse y es código contradictorio/muerto.
        [Required(ErrorMessage = "Nombre Usuario es obligatoria")]
        public required string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        public required string Correo { get; set; }

        [Required(ErrorMessage = "La Contraseña es obligatoria")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        public required string Rol { get; set; }

        public string? Foto { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public bool Estado { get; set; } = true;

        public DateTime? UltimoAcceso { get; set; }
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}