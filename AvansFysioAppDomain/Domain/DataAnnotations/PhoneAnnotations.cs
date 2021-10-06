using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysioAppDomain.Domain.DataAnnotations
{
    public class PhoneAnnotations : ValidationAttribute
    {
        public PhoneAnnotations() { }

        public override bool IsValid(object value)
        {
            string phone = (string) value;
            
            return phone.Length >= 10 && phone.Length <= 15;
        }
    }
}
