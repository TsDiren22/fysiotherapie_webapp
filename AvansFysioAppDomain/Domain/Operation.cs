using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFysioAppDomain.Domain
{
    public class Operation
    {
        [Key]
        public string Value { get; set; }
        public string Description { get; set; }
        public bool MandatoryExplanation { get; set; }
    }
}
