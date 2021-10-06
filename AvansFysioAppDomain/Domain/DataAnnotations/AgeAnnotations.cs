using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysioAppDomain.Domain.DataAnnotations
{
    public class AgeAnnotions : ValidationAttribute 
    {
        public AgeAnnotions(){}

        public override bool IsValid(object value)
        {
            if (value != null) return (DateTime)value <= DateTime.Now.AddYears(-16);
            return false;
        }
    }
}
