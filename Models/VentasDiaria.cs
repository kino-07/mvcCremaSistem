namespace mvcCremaSistem.Models
{
    public class VentasDiaria
    {
        public DateTime Fecha { get; set; }
        public int TotalPedidos { get; set; }
        public decimal TotalVentas { get; set; }
        public int UsuariosUnicos { get; set; }
        public decimal PromedioPedido { get; set; }
    }
}