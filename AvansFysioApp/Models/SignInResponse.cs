using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysioApp.Models
{
    public class SignInResponse
    {
        public bool succes { get; set; }
        public string token { get; set; } = string.Empty;
    }
}
