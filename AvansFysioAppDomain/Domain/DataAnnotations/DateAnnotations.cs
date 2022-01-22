using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFysioAppDomain.Domain.DataAnnotations
{
    class DateAnnotations : ValidationAttribute
    {
        public DateAnnotations() { }

        public override bool IsValid(object value)
        {
            return (DateTime)value > DateTime.Now;
        }
    }
}
