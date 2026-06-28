namespace mvcCremaSistem.Models
{
    public class PedidosCompleto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioEmail { get; set; }
        public string UsuarioTelefono { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public string MetodoPago { get; set; }
        public string EstadoPago { get; set; }
    }
}