namespace mvcCremaSistem.Models
{
    public class ProductosCompleto
    {
        public int Id { get; set; }
        public string ProductoNombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public int CategoriaId { get; set; }
        public string Categoria { get; set; }
        public bool Estado { get; set; }
        public bool Destacado { get; set; }

        public DateTime FechaCreacion { get; set; }
        public decimal? PrecioMinimo { get; set; }
        public decimal? PrecioMaximo { get; set; }
        public int CantidadGramajes { get; set; }
    }
}