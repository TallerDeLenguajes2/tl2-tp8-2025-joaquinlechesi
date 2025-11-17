using Microsoft.Data.Sqlite;
public class ProductoRepository
{
    private string cadenaConexion = "Data source = Db/Tienda.db";
    // Crear un nuevo Producto
    public void nuevoProducto(Productos nuevoProducto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string query = "INSERT INTO Productos (Descripcion, Precio) VALUES (@descripcion, @precio)";
        using var comando = new SqliteCommand(query, conexion);
        comando.Parameters.Add(new SqliteParameter("@descripcion", nuevoProducto.Description));
        comando.Parameters.Add(new SqliteParameter("@precio", nuevoProducto.Precio));
        comando.ExecuteNonQuery();
        conexion.Close();
    }
    // Modificar un Producto existente
    public void modificarProducto(int id, Productos nuevoProducto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string query = "UPDATE Productos SET Descripcion = @descripcion, Precio = @precio WHERE idProducto = @id";
        using var comando = new SqliteCommand(query, conexion);
        comando.Parameters.Add(new SqliteParameter("@descripcion", nuevoProducto.Description));
        comando.Parameters.Add(new SqliteParameter("@precio", nuevoProducto.Precio));
        comando.Parameters.Add(new SqliteParameter("@id", id));
        comando.ExecuteNonQuery(); //funciona
        conexion.Close();
    }
    // Listar todos los Productos registrados
    public List<Productos> GetAll() //Listar todos los productos registrados
    {
        string query = "SELECT * FROM Productos";
        List<Productos> productos = [];
        using var conecction = new SqliteConnection(cadenaConexion);
        conecction.Open();

        var command = new SqliteCommand(query, conecction);

        using (SqliteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                // var producto = new Productos
                // {
                //     Id = Convert.ToInt32(reader["idProducto"]),
                //     Descripcion = reader["Descripcion"].ToString(),
                //     Precio = Convert.ToInt32(reader["Precio"])
                // };
                var producto = new Productos();
                producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                producto.Description = reader["Descripcion"].ToString();
                producto.Precio = Convert.ToInt32(reader["Precio"]);
                productos.Add(producto);
            }
        }
        conecction.Close();
        return productos;
    }
    // Obtener detalles de un Producto por su ID
    public Productos GetById(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string query = "SELECT * FROM Productos WHERE idProducto = @id";
        using var comando = new SqliteCommand(query, conexion);
        comando.Parameters.Add(new SqliteParameter("@id", id));
        using var lector = comando.ExecuteReader();
        if (lector.Read())
        {
            var producto = new Productos
            {
                IdProducto = Convert.ToInt32(lector["idProducto"]),
                Description = lector["Descripcion"].ToString(),
                Precio = Convert.ToInt32(lector["Precio"])
            };
            return producto;
        }
        conexion.Close();
        return null; //consultar
    }
    // Eliminar un Producto
    public void DeleteById(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string query = "DELETE FROM Productos WHERE idProducto = @id";
        using var comando = new SqliteCommand(query, conexion);
        comando.Parameters.Add(new SqliteParameter("@id", id));
        comando.ExecuteNonQuery();

        conexion.Close();
    }
}
namespace MiWebAPI
{
    
}