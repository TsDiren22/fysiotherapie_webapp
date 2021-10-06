using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysioAppDomain.Domain.DataAnnotations
{
    public class OccupationAnnotations : ValidationAttribute
    {
        public OccupationAnnotations(){}

        public override bool IsValid(object value)
        {
            string occupation = (string)value;

            return !occupation.Equals("Select an occupation");
        }
    }
}
