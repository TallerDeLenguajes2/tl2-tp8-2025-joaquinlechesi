using MiWebAPI.Models;

namespace MiWebAPI.ViewModels.ProductoViewModel
{
    public class PresupuestosViewModel
    {
        public int IdPresupuestos { get; set; }
        public string NombreDestinatario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public PresupuestosViewModel()
        {
            
        }
        public PresupuestosViewModel(Presupuestos presupuesto)
        {
            IdPresupuestos = presupuesto.IdPresupuestos;
            NombreDestinatario = presupuesto.NombreDestinatario;
            FechaCreacion = presupuesto.FechaCreacion;
        }
    }
}