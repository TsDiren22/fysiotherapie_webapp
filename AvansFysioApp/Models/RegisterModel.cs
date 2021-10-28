using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysioApp.Models
{
    public class RegisterModel
    {
            
        [Required(ErrorMessage = "Please enter your username!")]
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your password!")]
        public string Password { get; set; }
    }
}
