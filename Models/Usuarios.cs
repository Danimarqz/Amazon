namespace Amazon.Models
{
    public class Usuarios
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Email { get; set; }
        public string userType { get; set; }
        public ICollection<Carrito>? Carrito { get; set; }
        public ICollection<Ventas>? Ventas { get; set; }
    }
}
