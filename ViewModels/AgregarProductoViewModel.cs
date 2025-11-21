using MiWebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MiWebAPI.ViewModels.AgregarProductoViewModel
{
    public class AgregarProductoViewModel
    {
        //[Required(ErrorMessage = "La descripcion es obligatoria.")]
        [StringLength(250)]
        public string Description { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0,100000)]
        public int Precio { get; set; }
    }
}