using System.ComponentModel.DataAnnotations;
using MiWebAPI.Models;

namespace MiWebAPI.ViewModels.LoginViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}