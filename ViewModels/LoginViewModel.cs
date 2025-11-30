using System.ComponentModel.DataAnnotations;
using MiWebAPI.Models;

namespace MiWebAPI.ViewModels.LoginViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe ingresar un usuario.")]
        public string ?Username { get; set; }
        [Required(ErrorMessage = "Debe ingresar un contrasenia."), DataType(DataType.Password)]
        public string ?Password { get; set; }
        public string ?ErrorMessage { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}