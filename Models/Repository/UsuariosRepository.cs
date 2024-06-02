using Dapper;
using System.Data;

namespace Amazon.Models.Repository
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly Conexion _conexion;
        public UsuariosRepository(Conexion conexion)
        {
            _conexion = conexion;
        }
        public async Task Add(Usuarios usuarios)
        {
            var query = @"
            INSERT INTO Usuarios (NombreUsuario, Contrasena, Email, userType) VALUES
            (@NombreUsuario, @Contrasena, @Email, @userType)";
            var parameters = new DynamicParameters();
            parameters.Add("NombreUsuario", usuarios.NombreUsuario, DbType.String);
            parameters.Add("Contrasena", usuarios.Contrasena, DbType.String);
            parameters.Add("Email", usuarios.Email, DbType.String);
            parameters.Add("userType", usuarios.userType, DbType.String);
            using (var connection = _conexion.ObtenerConexion())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task Delete(int id)
        {
            var query = "DELETE FROM Usuarios WHERE UsuarioID = @UsuarioID";
            var parameters = new DynamicParameters();
            parameters.Add("UsuarioID", id);
            using (var connection = _conexion.ObtenerConexion())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task Update(Usuarios usuarios)
        {
            var query = @"UPDATE Usuarios SET NombreUsuario = @NombreUsuario, 
            Contrasena = @Contrasena,
            Email = @Email,
            userType = @userType
            WHERE UsuarioID = @UsuarioID";
            var parameters = new DynamicParameters();
            parameters.Add("NombreUsuario", usuarios.NombreUsuario, DbType.String);
            parameters.Add("Contrasena", usuarios.Contrasena, DbType.String);
            parameters.Add("Email", usuarios.Email, DbType.String);
            parameters.Add("userType", usuarios.userType, DbType.String);
            parameters.Add("UsuarioID", usuarios.UsuarioID, DbType.Int32);
            using (var connection = _conexion.ObtenerConexion())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<Usuarios>> GetAll()
        {
            var query = "SELECT * FROM Usuarios";
            using (var connection = _conexion.ObtenerConexion())
            {
                return await connection.QueryAsync<Usuarios>(query);
            }
        }
        public async Task<Usuarios> GetById(int id)
        {
            var query = "SELECT * FROM Usuarios u WHERE u.UsuarioID = @UsuarioID";
            var parameters = new DynamicParameters();
            parameters.Add("UsuarioID", id, DbType.Int32);
            using (var connection = _conexion.ObtenerConexion())
            {
                return await connection.QuerySingleOrDefaultAsync<Usuarios>(query, parameters);
            }
        }

        public async Task<Usuarios> GetByEmail(string email)
        {
            var query = "SELECT * FROM Usuarios WHERE email = @email";
            var parameters = new DynamicParameters();
            parameters.Add("email", email, DbType.String);
            using (var connection = _conexion.ObtenerConexion())
            {
                return await connection.QuerySingleOrDefaultAsync<Usuarios>(query, parameters);
            }
        }
    }
}
