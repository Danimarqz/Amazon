using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Amazon.Models
{
    public class Usuarios
    {
        
        public int UsuarioID { get; set; }
        public string? NombreUsuario { get; set; }
        [Required]
        public string Contrasena { get; set; }
        [Key]
        [Required]
        public string Email { get; set; }
        public string userType { get; set; } = "usuario";
        public ICollection<Carrito>? Carrito { get; set; }
        public ICollection<Ventas>? Ventas { get; set; }
    }
}
