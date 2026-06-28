using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace mvcCremaSistem.Models
{
    public class Configuracion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Clave { get; set; }

        [Required]
        public string Valor { get; set; }

        [StringLength(200)]
        public string? Descripcion { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaActualizacion { get; set; }

        // NOTA: se removieron los métodos Crear/Editar/Eliminar.
        // Una entidad de EF Core debería ser solo el modelo de datos;
        // esa lógica de negocio pertenece a un servicio o repositorio
        // (ej. ConfiguracionService) que reciba el DbContext, no a la
        // clase que se mapea a la tabla. Dejarla acá puede confundir
        // a EF Core si en algún momento se intenta usar constructores
        // o inicializadores complejos al mapear la entidad.
    }
}