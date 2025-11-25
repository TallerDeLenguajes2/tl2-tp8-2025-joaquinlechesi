using Microsoft.AspNetCore.Mvc.Rendering;
using MiWebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MiWebAPI.ViewModels.AgregarProductoViewModel
{
    public class AgregarProductoViewModel
    {
        //[Required]
        public int IdPresupuestos { get; set; }

        [Display(Name = "Producto a agregar")]
        public int IdProducto { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero.")] //De manera provisoria dejamos como rango maximo 100
        public int Cantidad { get; set; }

        public SelectList ListaProductos { get; set; }

        // public AgregarProductoViewModel() //Dejo el constructor por precaucion
        // {
            
        // }
    }
}