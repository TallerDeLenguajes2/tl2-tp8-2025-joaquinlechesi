using MiWebAPI.Interfaces;
using MiWebAPI.Models;
using Microsoft.Data.Sqlite;
namespace MiWebAPI.Repository.UsuarioRepository;

public class UsuarioRepository : IUserRepository
{
    private string cadenaConexion = "Data source = Db/Tienda.db";
    public Usuario GetUser(string username, string password)
    {
        Usuario user = null;

        //Consulta SQL que busca por Usuario Y Contrasena
        const string sql = @"
                            SELECT Id, Nombre, User, Pass, Rol
                            FROM Usuarios
                            WHERE User = @Usuario AND Pass = @Contrasena";
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        using var comando = new SqliteCommand(sql, conexion);
        // Se usan parámetros para prevenir inyección SQL
        comando.Parameters.AddWithValue("@Usuario", username);
        comando.Parameters.AddWithValue("@Contrasena", password);
        using var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            // Si el lector encuentra una fila, el usuario existe y las credenciales son correctas
            user = new Usuario
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                User = reader.GetString(2),
                Pass = reader.GetString(3),
                Rol = reader.GetString(4)
            };
        }

        return user;
    }
}