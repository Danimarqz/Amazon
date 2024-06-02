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
    //Carrito
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
    public async Task<int> GetCartID(int userID)
    {
        string query = "SELECT CarritoID FROM Carrito WHERE UsuarioID = @UsuarioID";
        var parameters = new DynamicParameters();
        parameters.Add("UsuarioID", userID);
        using (var connection = _conexion.ObtenerConexion())
        {
            int? carritoID = await connection.QuerySingleOrDefaultAsync<int?>(query, parameters);
            return carritoID ?? 0;
        }
    }

    public async Task<IEnumerable<Carrito>> GetCartAdmin()
    {
        string query = "SELECT * FROM Carrito";
        using (var connection = _conexion.ObtenerConexion())
        {
            var AllCarritos = await connection.QueryAsync<Carrito>(query);
            return AllCarritos;
        }
    }
    public async Task Add(int userID)
    {
        DateTime now = DateTime.Now;
        int total = 0;
        string query = @"INSERT INTO Carrito (UsuarioID, FechaCarrito, totalVenta) 
                     VALUES (@UsuarioID, @FechaCarrito, @TotalVenta)";
        var parameters = new DynamicParameters();
        parameters.Add("UsuarioID", userID);
        parameters.Add("FechaCarrito", now);
        parameters.Add("TotalVenta", total);
        using (var connection = _conexion.ObtenerConexion())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task EditCarrito(Carrito cart)
    {
        decimal totalVenta = await GetCartPrice(cart.CarritoID);
        string query = @$"UPDATE Carrito SET
        totalVenta = {totalVenta},
        WHERE CarritoID = @CarritoID,
        AND UsuarioID = @UsuarioID";
        var parameters = new DynamicParameters();
        parameters.Add("totalVenta", cart.totalVenta, System.Data.DbType.Decimal);
        parameters.Add("CarritoID", cart.CarritoID, System.Data.DbType.Int32);
        parameters.Add("UsuarioID", cart.UsuarioID, System.Data.DbType.Int32);
        using (var connection = _conexion.ObtenerConexion())
        {
            connection.Execute(query, parameters);
        }
    }
    public async Task UpdateCart(int cartID)
    {
        decimal totalPrice = await GetCartPrice(cartID);
        var query = $@"UPDATE Carrito SET
        totalVenta = @totalVenta
        WHERE CarritoID = {cartID}";
        var parameters = new DynamicParameters();
        parameters.Add("totalVenta", totalPrice);
        using (var connection = _conexion.ObtenerConexion())
        {
            connection.Execute(query, parameters);
        }
    }
    //DetallesCarrito
    public async Task<IEnumerable<DetallesCarrito>> GetAllCart(int cartID)
    {
        string query = "SELECT * FROM DetallesCarrito WHERE CarritoID = @cartID";
        var paremeters = new DynamicParameters();
        paremeters.Add("cartID", cartID);
        using (var connection = _conexion.ObtenerConexion())
        {
            var cartDetailed = await connection.QueryAsync<DetallesCarrito>(query, paremeters);
            return cartDetailed;
        }
    }

    public async Task AddProducto(int productoID, int userID)
    {
        int cartID = await GetCartID(userID);
        int cantidad = await CheckCantidadProducto(productoID, cartID) + 1;
        decimal precio = await CheckPrecioUnitario(productoID);
        decimal precioTotal = precio * (cantidad);
        //UPDATE A CARRITOID
        int checkCartID = await CheckCarritoIDDetails(cartID);
        string query;
        if (checkCartID == 0)
        {
            query = @$"INSERT INTO DetallesCarrito (CarritoID, ProductoID, Cantidad, PrecioUnitario, PrecioTotal)
        VALUES ({cartID}, {productoID}, {cantidad}, @PrecioUnitario, @PrecioTotal)";
        } else
        {
            query = $@"UPDATE DetallesCarrito SET
            ProductoID = {productoID},
            Cantidad = {cantidad},
            PrecioUnitario = @PrecioUnitario,
            PrecioTotal = @PrecioTotal
            WHERE CarritoID = {cartID}";
        }
        var parameters = new DynamicParameters();
        parameters.Add("PrecioUnitario", precio, System.Data.DbType.Decimal);
        parameters.Add("PrecioTotal", precioTotal, System.Data.DbType.Decimal);
        using (var connection = _conexion.ObtenerConexion())
        {
            connection.Execute(query, parameters);
        }
        await UpdateCart(cartID);
    }
    public async Task RmProducto(int productoID, int userID)
    {
        int cartID = await GetCartID(userID);
        int cantidad = await CheckCantidadProducto(productoID, cartID) -1;
        if (cantidad <= 0)
        {
            await DeleteProducto(productoID, cartID);
        } else
        {
            decimal precio = await CheckPrecioUnitario(productoID);
            decimal precioTotal = precio * (cantidad);
            string query = $@"UPDATE DetallesCarrito SET
            ProductoID = {productoID},
            Cantidad = {cantidad},
            PrecioUnitario = @PrecioUnitario,
            PrecioTotal = @PrecioTotal
            WHERE CarritoID = {cartID}";
            var parameters = new DynamicParameters();
            parameters.Add("PrecioUnitario", precio, System.Data.DbType.Decimal);
            parameters.Add("PrecioTotal", precioTotal, System.Data.DbType.Decimal);
            using (var connection = _conexion.ObtenerConexion())
            {
                connection.Execute(query, parameters);
            }
        }
            await UpdateCart(cartID);
    }
    public async Task EditDetallesProductoCarrito(DetallesCarrito detallesCarrito)
    {
        string query = @"UPDATE DetallesCarrito SET
        Cantidad = @Cantidad,
        PrecioUnitario = @PrecioUnitario,
        PrecioTotal = @PrecioTotal
        WHERE CarritoID = @CarritoID
        AND ProductoID = @ProductoID";
        var parameters = new DynamicParameters();
        parameters.Add("ProductoID", detallesCarrito.ProductoID);
        parameters.Add("CarritoID", detallesCarrito.CarritoID);
        parameters.Add("Cantidad", detallesCarrito.Cantidad);
        parameters.Add("PrecioUnitario", detallesCarrito.PrecioUnitario);
        parameters.Add("PrecioTotal", detallesCarrito.PrecioTotal);
        using (var connection = _conexion.ObtenerConexion())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }


    public async Task DeleteProducto(int productoID, int carritoID)
    {
        string query = $"DELETE FROM DetallesCarrito WHERE carritoID = {carritoID} AND productoID = {productoID}";
        using (var connection = _conexion.ObtenerConexion())
        {
            await connection.ExecuteAsync(query);
        }
    }
    //Private check methods
    private async Task<int> CheckCantidadProducto(int productoID, int carritoID)
    {
        var query = $"SELECT Cantidad FROM DetallesCarrito WHERE CarritoID = {carritoID} AND productoID = {productoID}";
        using (var connection = _conexion.ObtenerConexion())
        {
            int? cantidad = await connection.ExecuteScalarAsync<int?>(query);
            return cantidad ?? 0;
        }
    }
    private async Task<decimal> CheckPrecioUnitario(int productoID)
    {
        var query = $"SELECT Precio FROM Productos WHERE ProductoID = {productoID}";
        using (var connection = _conexion.ObtenerConexion())
        {
            decimal? precio = await connection.ExecuteScalarAsync<decimal?>(query);
            return precio ?? 0;
        }
    }

    private async Task<decimal> GetCartPrice(int cartID)
    {
        var query = $"SELECT SUM(PrecioTotal) FROM DetallesCarrito WHERE CarritoID = {cartID}";
        using (var connection = _conexion.ObtenerConexion())
        {
            decimal total = await connection.ExecuteScalarAsync<decimal>(query);
            return total;
        }
    }
    private async Task<int> CheckCarritoIDDetails(int carritoID)
    {
        string query = "SELECT COUNT(*) FROM DetallesCarrito WHERE CarritoID = @CarritoID";
        var parameters = new DynamicParameters();
        parameters.Add("@CarritoID", carritoID);

        using (var connection = _conexion.ObtenerConexion())
        {
            int count = await connection.ExecuteScalarAsync<int>(query, parameters);
            return count;
        }
    }

}
