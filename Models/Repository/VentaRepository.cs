
using Dapper;

namespace Amazon.Models.Repository
{
    public class VentaRepository : IVentaRepository
    {
        private readonly Conexion _conexion;
        public VentaRepository(Conexion conexion)
        {
            _conexion = conexion;
        }

        public async Task AddVenta(int productoID, int userID)
        {
            int ventaID = await GetVentaID(userID);
            if (ventaID == 0)
            {
                await Add(userID);
                ventaID = await GetVentaID(userID);
            }
            int cantidad = await CheckCantidadProducto(productoID, ventaID) +1;
            decimal precio = await CheckPrecioUnitario(productoID);
            decimal precioTotal = precio * cantidad;
            int checkVentaDetailsID = await CheckVentaIDDetails(ventaID);
            string query;
            if(checkVentaDetailsID == 0 || cantidad <= 1)
            {
                query = $@"INSERT INTO DetallesVenta (VentaID, ProductoID, Cantidad, PrecioUnitario, PrecioTotal)
                VALUES ({ventaID}, {productoID}, {cantidad}, @PrecioUnitario, @PrecioTotal)";
            } else
            {
                query = $@"UPDATE DetallesVenta SET
                Cantidad = {cantidad},
                PrecioUnitario = @PrecioUnitario,
                PrecioTotal = @PrecioTotal
                WHERE VentaID = {ventaID}
                AND ProductoID = {productoID}";
            }
            var parameters = new DynamicParameters();
            parameters.Add("PrecioUnitario", precio);
            parameters.Add("PrecioTotal", precioTotal);
            using (var connection = _conexion.ObtenerConexion())
            {
                connection.Execute(query, parameters);
            }
            await UpdateVenta(ventaID);
        }

        public async Task EditVenta(DetallesVenta detallesVenta)
        {
            string query = @$"UPDATE DetallesVenta SET
            Cantidad = {detallesVenta.Cantidad},
            PrecioUnitario = @PrecioUnitario,
            PrecioTotal = @PrecioTotal
            WHERE VentaID = {detallesVenta.VentaID}
            AND ProductoID = {detallesVenta.ProductoID}";
            var parameters = new DynamicParameters();
            parameters.Add("PrecioUnitario", detallesVenta.PrecioUnitario);
            parameters.Add("PrecioTotal", detallesVenta.PrecioTotal);
            using (var connection = _conexion.ObtenerConexion())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<DetallesVenta>> GetDetallesVenta(int ventaID)
        {
            string query = $"SELECT * FROM DetallesVenta WHERE VentaID = {ventaID}";
            using (var connection = _conexion.ObtenerConexion())
            {
                var ventaDetail = await connection.QueryAsync<DetallesVenta>(query);
                return ventaDetail;
            }
        }

        public async Task<int> GetVentaID(int userID)
        {
            string query = $"SELECT VentaID FROM Ventas WHERE UsuarioID = {userID}";
            using (var connection = _conexion.ObtenerConexion())
            {
                int? ventaID = await connection.QuerySingleOrDefaultAsync<int?>(query);
                return ventaID ?? 0;
            }
        }

        public async Task<IEnumerable<Ventas>> GetVentasAdmin()
        {
            string query = "SELECT * FROM Ventas";
            using (var connection = _conexion.ObtenerConexion())
            {
                var AllVentas = await connection.QueryAsync<Ventas>(query);
                return AllVentas;
            }
        }

        public async Task RmVenta(int productoID, int userID)
        {
            int ventaID = await GetVentaID(userID);
            int cantidad = await CheckCantidadProducto(productoID, ventaID) - 1;
            if (cantidad < 1)
            {
                await DeleteProducto(productoID, ventaID);
            }
            else
            {
                decimal precio = await CheckPrecioUnitario(productoID);
                decimal precioTotal = precio * cantidad;
                string query = $@"UPDATE DetallesVenta SET
                ProductoID = {productoID},
                Cantidad = {cantidad},
                PrecioUnitario = @PrecioUnitario,
                PrecioTotal = @PrecioTotal
                WHERE VentaID = {ventaID}";
                var parameters = new DynamicParameters();
                parameters.Add("PrecioUnitario", precio);
                parameters.Add("PrecioTotal", precioTotal);
                using (var connection = _conexion.ObtenerConexion())
                {
                    connection.Execute(query, parameters);
                }
            }
            await UpdateVenta(ventaID);
        }
        private async Task Add(int userID)
        {
            DateTime now = DateTime.Now;
            int total = 0;
            string query = @$"INSERT INTO Ventas (UsuarioID, FechaVenta, totalVenta)
            VALUES ({userID}, @FechaCarrito, @total)";
            var parameters = new DynamicParameters();
            parameters.Add("FechaCarrito", now);
            parameters.Add("total", total);
            using (var connection = _conexion.ObtenerConexion())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        private async Task UpdateVenta(int ventaID)
        {
            decimal totalPrice = await GetVentaPrice(ventaID);
            var query = $@"UPDATE Ventas SET
            TotalVenta = @totalPrice
            WHERE VentaID = {ventaID}";
            var parameters = new DynamicParameters();
            parameters.Add("totalPrice", totalPrice);
            using (var connection = _conexion.ObtenerConexion())
            {
                connection.Execute(query, parameters);
            }
        }
        private async Task<decimal> GetVentaPrice(int ventaID)
        {
            var query = $"SELECT SUM(PrecioTotal) FROM DetallesVenta WHERE VentaID = {ventaID}";
            using (var connection = _conexion.ObtenerConexion())
            {
                decimal total = await connection.ExecuteScalarAsync<decimal>(query);
                return total;
            }
        }
        private async Task DeleteProducto(int productoID, int ventaID)
        {
            string query = $"DELETE FROM DetallesVenta WHERE VentaID = {ventaID} AND ProductoID = {productoID}";
            using (var connection = _conexion.ObtenerConexion())
            {
                await connection.ExecuteAsync(query);
            }
        }
        private async Task<int> CheckCantidadProducto(int productoID, int ventaID)
        {
            var query = $"SELECT Cantidad FROM DetallesVenta WHERE VentaID = {ventaID} AND ProductoID = {productoID}";
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
        private async Task<int> CheckVentaIDDetails(int ventaID)
        {
            string query = $"SELECT COUNT(*) FROM DetallesVenta WHERE VentaID = {ventaID}";
            using (var connection = _conexion.ObtenerConexion())
            {
                int? count = await connection.ExecuteScalarAsync<int?>(query);
                return count ?? 0;
            }
        }
    }
}
