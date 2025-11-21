using MiWebAPI.ViewModels.AgregarProductoViewModel;
using MiWebAPI.ViewModels.ProductoViewModel;
namespace MiWebAPI.Models;
public class Productos
{
    public int IdProducto { get; set; }
    public string Description { get; set; }
    public int Precio { get; set; }
    public Productos()
    {
        
    }
    public Productos(AgregarProductoViewModel productoVM)
    {
        Description = productoVM.Description;
        Precio = productoVM.Precio;
    }

}