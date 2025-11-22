using System.ComponentModel.DataAnnotations;
using MiWebAPI.Models;

namespace MiWebAPI.ViewModels.PresupuestosViewModel
{
    public class EditarPresupuestosViewModel
    {
        [Required]
        public int IdPresupuestos { get; set; }

        [Required(ErrorMessage = "El nombre no puede ser vacío.")]
        public string NombreDestinatario { get; set; }

        [EmailAddress(ErrorMessage = "Debe tener formato de correo electronico")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Debe ingresar una fecha.")][DataType(DataType.Date)] //Falta controlar que la fecha sea menor a la actual
        public DateTime FechaCreacion { get; set; }
        
        public EditarPresupuestosViewModel()
        {
            
        }
        public EditarPresupuestosViewModel(Presupuestos presupuesto)
        {
            IdPresupuestos = presupuesto.IdPresupuestos;
            NombreDestinatario = presupuesto.NombreDestinatario;
            FechaCreacion = presupuesto.FechaCreacion;
        }
    }
}