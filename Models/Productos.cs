namespace Amazon.Models
{
    public class Productos
    {
        public int ProductoID { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public ICollection<DetallesCarrito> DetallesCarrito { get; set; }
        public ICollection<DetallesVenta> DetallesVenta { get; set; }
    }
}
