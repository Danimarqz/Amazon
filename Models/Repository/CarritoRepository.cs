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

    public void Add(Carrito cart)
    {
        using (var connection = _conexion.ObtenerConexion())
        {
            string query = @"INSERT INTO Carts (UsuarioID, FechaCarrito, totalVenta) 
            VALUES (@UserID, @FechaCarrito, @totalVenta)";
            var parameters = new DynamicParameters();
            parameters.Add("UserID", cart.UsuarioID, System.Data.DbType.Int32);
            parameters.Add("FechaCarrito", cart.FechaCarrito, System.Data.DbType.DateTime);
            parameters.Add("totalVenta", cart.totalVenta, System.Data.DbType.Decimal);
            connection.Execute(query, parameters);
        }
    }
    public void Edit(Carrito cart)
    {
        string query = @"UPDATE Carrito SET
        FechaCarrito = @FechaCarrito
        totalVenta = @totalVenta
        WHERE CarritoID = @CarritoID
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

    public void Delete(Carrito cart)
    {
        string query = "DELETE FROM Carrito WHERE CarritoID = @CarritoID AND UsuarioID = @UsuarioID";
        var parameters = new DynamicParameters();
        parameters.Add("CarritoID", cart.CarritoID, System.Data.DbType.Int32);
        parameters.Add("UsuarioID", cart.UsuarioID, System.Data.DbType.Int32);
        using (var connection = _conexion.ObtenerConexion())
        {
            connection.Execute(query, parameters);
        }

    }
}
