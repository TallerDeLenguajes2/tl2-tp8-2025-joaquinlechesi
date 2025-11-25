using System.ComponentModel.DataAnnotations;
using MiWebAPI.Models;

namespace MiWebAPI.ViewModels.ProductoViewModel
{
    public class EditarProductoViewModel
    {
        [Required]
        public int IdProducto { get; set; }

        [StringLength(250)] //Falta agregar el mensaje para la longitud
        public string Description { get; set; }
        
        [Required(ErrorMessage = "La cantidad es obligatoria")][Range(1,10000)] //Falta agregar el mensaje para el rango
        public int Precio { get; set; }

        public EditarProductoViewModel()
        {
            
        }
        public EditarProductoViewModel(Productos producto)
        {
            IdProducto = producto.IdProducto;
            Description = producto.Description;
            Precio = producto.Precio;
        }
    }
}