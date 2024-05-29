namespace Amazon.Models
{
    public class DetallesVenta
    {
        public int DetalleID { get; set; }
        public int VentaID { get; set; }
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double PreicoTotal { get; set; }
        public Ventas Ventas { get; set; }
        public Productos Productos { get; set; }
    }
}
