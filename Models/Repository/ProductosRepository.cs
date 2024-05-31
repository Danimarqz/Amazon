
using Dapper;
using System.Data;

namespace Amazon.Models.Repository
{
    public class ProductosRepository : IProductosRepository
    {
        private readonly Conexion _conexion;

        public ProductosRepository(Conexion conexion)
        {
            _conexion = conexion;
        }
        public void Create(Productos productos)
        {
            var query = @"
            INSERT INTO Productos (Precio, Descripcion, Nombre)
            VALUES (@Precio, @Descripcion, @Nombre";
            var parameters = new DynamicParameters();
            parameters.Add("Precio", productos.Precio, DbType.Double);
            parameters.Add("Descripcion", productos.Descripcion, DbType.String);
            parameters.Add("Nombre", productos.Nombre, DbType.String);
            using (var connection = _conexion.ObtenerConexion())
            {
                connection.Execute(query, parameters);
            }
        }

        public void Delete(Productos productos)
        {
            var query = "DELETE FROM Productos WHERE ProductoID = @ProductoID";
            var parameters = new DynamicParameters();
            parameters.Add("ProductoID", productos.ProductoID, DbType.Int32);
            using (var connection = _conexion.ObtenerConexion())
            {
                connection.Execute(query, parameters);
            }
        }

        public Task<IEnumerable<Productos>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Productos> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Productos productos)
        {
            throw new NotImplementedException();
        }
    }
}
