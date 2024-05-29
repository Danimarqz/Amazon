using Amazon.Models;
using Amazon.Models.Repository;
using Dapper;

public class CarritoRepository : ICarritoRepository
{
    private readonly Conexion _conexion;

    public CarritoRepository(Conexion conexion)
    {
        _conexion = conexion;
    }

    public IEnumerable<Carrito> GetCartsWithUsers(int userID)
    {
        using (var connection = _conexion.ObtenerConexion())
        {
            string query = @$"
                SELECT c.*, u.*
                FROM Carts c
                INNER JOIN Usuarios u ON c.UsuarioID = u.UsuarioID
                WHERE u.UserID = {userID}";

            var cartDictionary = new Dictionary<int, Carrito>();

            var result = connection.Query<Carrito, Usuarios, Carrito>(query,
                (cart, user) =>
                {
                    if (!cartDictionary.TryGetValue(cart.CarritoID, out var currentCart))
                    {
                        currentCart = cart;
                        cartDictionary.Add(currentCart.CarritoID, currentCart);
                    }

                    currentCart.usuario = user;
                    return currentCart;
                }, splitOn: "UserID");

            return result.Distinct().ToList();
        }
    }

    public void AddCart(Carrito cart)
    {
        using (var connection = _conexion.ObtenerConexion())
        {
            string query = "INSERT INTO Carts (UsuarioID, FechaCarrito) VALUES (@UserID, @DateCreated)";
            connection.Execute(query, cart);
        }
    }

    // Otros métodos para manejar el carrito
}
