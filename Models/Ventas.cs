namespace Amazon.Models
{
    public class Ventas
    {
        public int VentaID { get; set; }
        public int UsuarioID { get; set; }
        public DateTime FechaVenta { get; set; }
        public double TotalVenta { get; set; }
        public Usuarios usuario { get; set; }
        public ICollection<DetallesVenta>? DetallesVenta { get; set; }
    }
}
