using MiWebAPI.Models;
using System.Linq.Expressions;
using Microsoft.Data.Sqlite;

public class PresupuestoRepository
{
    private string cadenaConexion = "Data source = Db/Tienda.db";
    // Crea un nuevo Presupuesto
    public void AltaPresupuesto(Presupuestos NuevoPresupuesto)
    {
        string query = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@nombre, @fecha)";
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        using var comando = new SqliteCommand(query, conexion);
        comando.Parameters.Add(new SqliteParameter("@nombre", NuevoPresupuesto.NombreDestinatario));
        comando.Parameters.Add(new SqliteParameter("@fecha", NuevoPresupuesto.FechaCreacion.ToString("yyyy-MM-dd")));
        comando.ExecuteNonQuery();

        conexion.Close();
    }
    // Lista todos los Presupuestos registrados
    public List<Presupuestos> GetAll()
    {
        string query = "SELECT * FROM Presupuestos";
        List<Presupuestos> presupuestos = new List<Presupuestos>();
        using var conecction = new SqliteConnection(cadenaConexion);
        conecction.Open();
        var command = new SqliteCommand(query, conecction);
        using (SqliteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var Presupuesto = new Presupuestos();
                Presupuesto.IdPresupuestos = Convert.ToInt32(reader["idPresupuestos"]);
                Presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                Presupuesto.FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                presupuestos.Add(Presupuesto);
            }
        }
        conecction.Close();
        return presupuestos;
    }
    // Obtener detalles de un Presupuesto por su ID
    public Presupuestos GetDetallesById(int id)
    {
        using var connection = new SqliteConnection(cadenaConexion);
        var presupuesto = new Presupuestos();
        connection.Open();
        string query = @"SELECT
                         p.idPresupuestos,
                         p.NombreDestinatario,
                         p.FechaCreacion,
                         pr.idProducto,
                         pr.Descripcion,
                         pr.Precio,
                         d.Cantidad
                         FROM Presupuestos p INNER JOIN PresupuestosDetalle d ON p.idPresupuestos = d.idPresupuesto
                                             INNER JOIN Productos pr ON d.idProducto = pr.idProducto
                         WHERE p.idPresupuestos = @id";
        using var command = new SqliteCommand(query, connection);
        command.Parameters.Add(new SqliteParameter("@id", id));

        using var lector = command.ExecuteReader();

        presupuesto = null;

        while (lector.Read())
        {

            if (presupuesto == null)
            {
                presupuesto = new Presupuestos()
                {
                    IdPresupuestos = Convert.ToInt32(lector["idPresupuestos"]),
                    NombreDestinatario = lector["NombreDestinatario"].ToString(),
                    FechaCreacion = Convert.ToDateTime(lector["FechaCreacion"]),
                    Detalle = new List<PresupuestosDetalle>()
                };
            }

            var nuevoProducto = new Productos()
            {
                IdProducto = Convert.ToInt32(lector["idProducto"]),
                Description = lector["Descripcion"].ToString(),
                Precio = Convert.ToInt32(lector["Precio"]),
            };

            var presupuestoDetalle = new PresupuestosDetalle()
            {
                productos = nuevoProducto,
                Cantidad = Convert.ToInt32(lector["Cantidad"])
            };

            presupuesto.Detalle.Add(presupuestoDetalle);
        }
        // if (lector.Read())
        // {
        //     presupuestos.NombreDestinatario = lector["NombreDestinatario"].ToString();
        //     presupuestos.FechaCreacion = DateTime.Parse(lector["FechaCreacion"].ToString());
        // }
        // connection.Close();

        return presupuesto;
    }
    //Obtener Presupuesto por ID sin los detalles
    public Presupuestos GetById(int id)
    {
        using var connection = new SqliteConnection(cadenaConexion);
        var presupuesto = new Presupuestos();
        connection.Open();
        string query = @"SELECT
                         p.idPresupuestos,
                         p.NombreDestinatario,
                         p.FechaCreacion
                         FROM Presupuestos p
                         WHERE p.idPresupuestos = @id";
        using var command = new SqliteCommand(query, connection);
        command.Parameters.Add(new SqliteParameter("@id", id));

        using var lector = command.ExecuteReader();

        presupuesto = null;
        if (lector.Read())
        {
            presupuesto = new Presupuestos()
            {
                IdPresupuestos = Convert.ToInt32(lector["idPresupuestos"]),
                NombreDestinatario = lector["NombreDestinatario"].ToString(),
                FechaCreacion = Convert.ToDateTime(lector["FechaCreacion"]),
                Detalle = new List<PresupuestosDetalle>()
            };
        }
        connection.Close();

        return presupuesto;
    }
    // Agregar un producto y una cantidad a un Presupuesto
    public void agregarAPresupuesto(int idPresupuesto, int idProducto, int cantidad)
    {
        using var connection = new SqliteConnection(cadenaConexion);
        connection.Open();
        string query = @"INSERT INTO PresupuestosDetalles (idPresupuesto, idProducto, Cantidad)
                         VALUES (@IdPresupuesto, @IdProducto, @cantidad)";
        using var command = new SqliteCommand(query, connection);
        command.Parameters.Add(new SqliteParameter("@IdPresupuesto", idPresupuesto));
        command.Parameters.Add(new SqliteParameter("@IdProducto", idProducto));
        command.Parameters.Add(new SqliteParameter("@Cantidad", cantidad));
        command.ExecuteNonQuery();

        connection.Close();
    }
    // Eliminar un Presupuesto por ID
    public void DeleteById(int idPresupuesto)
    {
        using var connection = new SqliteConnection(cadenaConexion);
        string query =@"DELETE FROM Presupuestos
                        WHERE idPresupuestos = @id";
        using var command = new SqliteCommand(query, connection);
        command.Parameters.Add(new SqliteParameter("@id", idPresupuesto));
        connection.Open();
        
        command.ExecuteNonQuery();

        connection.Close();
    }
    // Modificar un presupuesto
    public void ModificarById(Presupuestos presupuesto)
    {
        using var connection = new SqliteConnection(cadenaConexion);
        string query =@"UPDATE Presupuestos
                        SET NombreDestinatario = @nombre, FechaCreacion = @fecha
                        WHERE idPresupuestos = @id";
        using var command = new SqliteCommand(query, connection);
        command.Parameters.Add(new SqliteParameter("@nombre", presupuesto.NombreDestinatario));
        command.Parameters.Add(new SqliteParameter("@fecha", presupuesto.FechaCreacion));
        command.Parameters.Add(new SqliteParameter("@id", presupuesto.IdPresupuestos));
        connection.Open();

        command.ExecuteNonQuery();

        connection.Close();
    }

    // public List<Productos> GetAll()
    // {
    //     string query = "SELECT * FROM Productos";
    //     List<Productos> productos = [];
    //     using var conecction = new SqliteConnection(cadenaConexion);
    //     conecction.Open();

    //     var command = new SqliteCommand(query, conecction);

    //     using (SqliteDataReader reader = command.ExecuteReader())
    //     {
    //         while (reader.Read())
    //         {
    //             // var producto = new Productos
    //             // {
    //             //     Id = Convert.ToInt32(reader["idProducto"]),
    //             //     Descripcion = reader["Descripcion"].ToString(),
    //             //     Precio = Convert.ToInt32(reader["Precio"])
    //             // };
    //             var producto = new Productos();
    //             producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
    //             producto.Description = reader["Descripcion"].ToString();
    //             producto.Precio = Convert.ToInt32(reader["Precio"]);
    //             productos.Add(producto);
    //         }
    //     }
    //     conecction.Close();
    //     return productos;
    // }
}