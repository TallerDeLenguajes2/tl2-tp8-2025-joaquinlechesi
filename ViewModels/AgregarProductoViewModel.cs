using MiWebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MiWebAPI.ViewModels.AgregarProductoViewModel
{
    public class AgregarProductoViewModel
    {
        [Required(ErrorMessage = "La descripcion es obligatoria.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        public int Precio { get; set; }
    }
}