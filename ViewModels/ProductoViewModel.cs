using MiWebAPI.Models;

namespace MiWebAPI.ViewModels.ProductoViewModel
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }
        public string Description { get; set; }
        public int Precio { get; set; }
        public ProductoViewModel()
        {
            
        }
        public ProductoViewModel(Productos producto)
        {
            IdProducto = producto.IdProducto;
            Description = producto.Description;
            Precio = producto.Precio;
        }
    }
}