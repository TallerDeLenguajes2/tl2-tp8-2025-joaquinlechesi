using MiWebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MiWebAPI.ViewModels.AgregarProductoViewModel
{
    public class AgregarProductoViewModel
    {
        [Required][Range(0,100)] //De manera provisoria dejamos como rango maximo 100
        public int Cantidad { get; set; }
    }
}