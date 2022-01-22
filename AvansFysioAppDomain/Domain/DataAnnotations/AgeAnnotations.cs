using System;
using System.ComponentModel.DataAnnotations;

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
