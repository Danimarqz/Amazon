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

    public async Task<Carrito> GetCart(int userID)
    {
            string query = "SELECT * FROM Carrito WHERE UsuarioID = @UsuarioID";
            var parameters = new DynamicParameters();
            parameters.Add("UsuarioID", userID, System.Data.DbType.Int32);
            using (var connection = _conexion.ObtenerConexion())
            {
                var carrito = await connection.QuerySingleOrDefaultAsync<Carrito>(query, parameters);
                return carrito;
            }
    }
    public async Task<int> GetCardID(int userID)
    {
        string query = "SELECT CarritoID FROM Carrito WHERE UsuarioID = @UsuarioID";
        var parameters = new DynamicParameters();
        parameters.Add("UsuarioID", userID);
        using (var connection = _conexion.ObtenerConexion())
        {
            int carritoID = await connection.QuerySingle(query, parameters);
            return carritoID;
        }
    }
    public async Task<DetallesCarrito> GetAllCart(int userID)
    {
        int cartID = await GetCardID(userID);
        string query = "SELECT * FROM DetallesCarrito WHERE CarritoID = @cartID";
        var paremeters = new DynamicParameters();
        paremeters.Add("cartID", cartID);
        using (var connection = _conexion.ObtenerConexion())
        {
            var cartDetailed = await connection.QuerySingleOrDefaultAsync<DetallesCarrito>(query, paremeters);
            return cartDetailed;
        }
    }

    public void Add(Carrito cart)
    {
        string query = @"INSERT INTO Carts (UsuarioID, FechaCarrito, totalVenta) 
        VALUES (@UserID, @FechaCarrito, @totalVenta)";
        var parameters = new DynamicParameters();
        parameters.Add("UserID", cart.UsuarioID, System.Data.DbType.Int32);
        parameters.Add("FechaCarrito", cart.FechaCarrito, System.Data.DbType.DateTime);
        parameters.Add("totalVenta", cart.totalVenta, System.Data.DbType.Decimal);
        using (var connection = _conexion.ObtenerConexion())
        {
            connection.Execute(query, parameters);
        }
    }
    public void AddDetalles(DetallesCarrito detallesCarrito)
    {
        string query = @"INSERT INTO DetallesCarrito (ProductoID, Cantidad, PrecioUnitario, PrecioTotal)
        VALUES (@ProductoID, @Cantidad, @PrecioUnitario, @PrecioTotal)";
        var parameters = new DynamicParameters();
        parameters.Add("ProductoID", detallesCarrito.ProductoID);
        parameters.Add("Cantidad", detallesCarrito.Cantidad);
        parameters.Add("PrecioUnitario", detallesCarrito.PrecioUnitario);
        parameters.Add("PrecioTotal", detallesCarrito.PrecioTotal);
        using (var connection = _conexion.ObtenerConexion())
        {
            connection.Execute(query, parameters);
        }
    }
    public void EditCarrito(Carrito cart)
    {
        string query = @"UPDATE Carrito SET
        FechaCarrito = @FechaCarrito,
        totalVenta = @totalVenta,
        WHERE CarritoID = @CarritoID,
        AND UsuarioID = @UsuarioID";
        var parameters = new DynamicParameters();
        parameters.Add("FechaCarrito", cart.FechaCarrito, System.Data.DbType.DateTime);
        parameters.Add("totalVenta", cart.totalVenta, System.Data.DbType.Decimal);
        parameters.Add("CarritoID", cart.CarritoID, System.Data.DbType.Int32);
        parameters.Add("UsuarioID", cart.UsuarioID, System.Data.DbType.Int32);
        using (var connection = _conexion.ObtenerConexion())
        {
            connection.Execute(query, parameters);
        }
    }
    public void EditDetallesCarrito(DetallesCarrito detallesCarrito)
    {
        string query = @"UPDATE DetallesCarrito SET
        ProductoID = @ProductoID,
        Cantidad = @Cantidad,
        PrecioUnitario = @PrecioUnitario,
        PrecioTotal = @PrecioTotal";
        var parameters = new DynamicParameters();
        parameters.Add("ProductoID", detallesCarrito.ProductoID);
        parameters.Add("Cantidad", detallesCarrito.Cantidad);
        parameters.Add("PrecioUnitario", detallesCarrito.PrecioUnitario);
        parameters.Add("PrecioTotal", detallesCarrito.PrecioTotal);
        using (var connection = _conexion.ObtenerConexion())
        {
            connection.Execute(query, parameters);
        }
    }
}
