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
        public void Add(Usuarios usuarios)
        {
            var query = @"
            INSERT INTO Usuarios (NombreUsuario, Contrasena, Email, userType) VALUES
            (@NombreUsuario, @Contrasena, @Email, @userType";
            var parameters = new DynamicParameters();
            parameters.Add("NombreUsuario", usuarios.NombreUsuario, DbType.String);
            parameters.Add("Contrasena", usuarios.Contrasena, DbType.String);
            parameters.Add("Email", usuarios.Email, DbType.String);
            parameters.Add("userType", usuarios.userType, DbType.String);
            using (var connection = _conexion.ObtenerConexion())
            {
                connection.Execute(query, parameters);
            }
        }

        public void Delete(Usuarios usuarios)
        {
            var query = "DELETE * FROM Usuarios u WHERE u.UsuarioID = @UsuarioID";
            var parameters = new DynamicParameters();
            parameters.Add("UsuarioID", usuarios.UsuarioID);
            using (var connection = _conexion.ObtenerConexion())
            {
                connection.Execute(query, parameters);
            }
        }

        public void Update(Usuarios usuarios)
        {
            var query = @"UPDATE Usuarios SET NombreUsuario = @NombreUsuario, 
            Contrasena = @Contrasena,
            Email = @Email
            userType = @userType";
            var parameters = new DynamicParameters();
            parameters.Add("NombreUsuario", usuarios.NombreUsuario, DbType.String);
            parameters.Add("Contrasena", usuarios.Contrasena, DbType.String);
            parameters.Add("Email", usuarios.Email, DbType.String);
            parameters.Add("userType", usuarios.userType, DbType.String);
            using (var connection = _conexion.ObtenerConexion())
            {
                connection.Execute(query, parameters);
            }
        }

        async Task<IEnumerable<Usuarios>> IUsuariosRepository.GetAll()
        {
            var query = "SELECT * FROM Usuarios";
            using (var connection = _conexion.ObtenerConexion())
            {
                return await connection.QueryAsync<Usuarios>(query);
            }
        }
    }
}
