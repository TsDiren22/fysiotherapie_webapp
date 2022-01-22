using System.ComponentModel.DataAnnotations;

namespace AvansFysioApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your username!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your password!")]
        public string Password { get; set; }
        public string Email { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
