using MiWebAPI.Models;
namespace MiWebAPI.Interfaces
{
    public interface IProductoRepository
    {
        void nuevoProducto(Productos nuevoProducto);
        void modificarProducto(int id, Productos nuevoProducto);
        List<Productos> GetAll();
        Productos GetById(int id);
        void DeleteById(int id);
    }
}