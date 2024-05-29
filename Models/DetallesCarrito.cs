namespace Amazon.Models
{
    public class DetallesCarrito
    {
        public int DetalleID { get; set; }
        public int CarritoID { get; set; }
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double PrecioTotal { get; set; }
        public Carrito carrito { get; set; }
        public Productos productos { get; set; }
    }
}
