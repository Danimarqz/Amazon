namespace Amazon.Models
{
    public class Carrito
    {
        public int CarritoID { get; set; }
        public int UsuarioID { get; set; }
        public DateTime FechaCarrito {  get; set; }
        public double totalVenta { get; set; }
        public Usuarios usuario { get; set; }
        public ICollection<DetallesCarrito>? DetallesCarrito { get; set;}
    }
}
