﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
