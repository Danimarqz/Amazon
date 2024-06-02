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
        if (cartID == 0)
        {
            await Add(userID);
            cartID = await GetCartID(userID);
        }
        int cantidad = await CheckCantidadProducto(productoID, cartID) + 1;
        decimal precio = await CheckPrecioUnitario(productoID);
        decimal precioTotal = precio * (cantidad);
        int checkCartDetailsID = await CheckCarritoIDDetails(cartID);
        string query;
        if (checkCartDetailsID == 0 || cantidad <= 1)
        {
            query = @$"INSERT INTO DetallesCarrito (CarritoID, ProductoID, Cantidad, PrecioUnitario, PrecioTotal)
        VALUES ({cartID}, {productoID}, {cantidad}, @PrecioUnitario, @PrecioTotal)";
        } else
        {
            query = $@"UPDATE DetallesCarrito SET
            Cantidad = {cantidad},
            PrecioUnitario = @PrecioUnitario,
            PrecioTotal = @PrecioTotal
            WHERE CarritoID = {cartID}
            AND ProductoID = {productoID}";
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
        if (cantidad < 1)
        {
            await DeleteProducto(productoID, cartID);
        } else
        {
            decimal precio = await CheckPrecioUnitario(productoID);
            decimal precioTotal = precio * (cantidad);
            string query = $@"UPDATE DetallesCarrito SET
            Cantidad = {cantidad},
            PrecioUnitario = @PrecioUnitario,
            PrecioTotal = @PrecioTotal
            WHERE CarritoID = {cartID}
            AND ProductoID = {productoID}";
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
    public async Task RmAllProducto(int productoID, int userID)
    {
        int cartID = await GetCartID(userID);
        await DeleteProducto(productoID, cartID);
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
    public async Task RmCarrito(int userID)
    {
        string query = $@"DELETE FROM Carrito WHERE UsuarioID = {userID}";
        await RemoveAllCarrito(userID);
        using (var connection = _conexion.ObtenerConexion())
        {
            await connection.ExecuteAsync(query);
        }
    }


    //Private methods
    private async Task Add(int userID)
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
    private async Task UpdateCart(int cartID)
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
    private async Task DeleteProducto(int productoID, int carritoID)
    {
        string query = $"DELETE FROM DetallesCarrito WHERE carritoID = {carritoID} AND productoID = {productoID}";
        using (var connection = _conexion.ObtenerConexion())
        {
            await connection.ExecuteAsync(query);
        }
    }
    private async Task RemoveAllCarrito(int userID)
    {
        int cartID = await GetCartID(userID);
        string query = $"DELETE FROM DetallesCarrito WHERE carritoID = {cartID}";
        using (var connection = _conexion.ObtenerConexion())
        {
            await connection.ExecuteAsync(query);
        }

    }
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
            int? count = await connection.ExecuteScalarAsync<int?>(query, parameters);
            return count ?? 0;
        }
    }
}
