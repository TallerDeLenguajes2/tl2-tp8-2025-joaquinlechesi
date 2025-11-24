using System.ComponentModel.DataAnnotations;
using MiWebAPI.Models;

namespace MiWebAPI.ViewModels.ProductoViewModel
{
    public class CrearProductoViewModel //Editar ProductoViewModel
    {
        //public int IdProducto { get; set; } //El ID lo maneja la base de datos
        [StringLength(250)]
        public string Description { get; set; }
        [Required][Range(0,10000)]
        public int Precio { get; set; }
        // public CrearProductoViewModel()
        // {
            
        // }
        // public CrearProductoViewModel(Productos producto)
        // {
        //     //IdProducto = producto.IdProducto;
        //     Description = producto.Description;
        //     Precio = producto.Precio;
        // }
    }
}