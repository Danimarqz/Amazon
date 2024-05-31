using System.ComponentModel.DataAnnotations;

namespace Amazon.Models
{
    public class Productos
    {
        public int ProductoID { get; set; }
        [Required]
        public double Precio { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Nombre { get; set; }
        public ICollection<DetallesCarrito>? DetallesCarrito { get; set; }
        public ICollection<DetallesVenta>? DetallesVenta { get; set; }
    }
}
