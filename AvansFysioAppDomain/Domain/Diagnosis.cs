using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFysioAppDomain.Domain
{
    public class Diagnosis
    {
        [Key]
        public string Code { get; set; }
        public string LocationOnBody { get; set; }
        public string Pathology { get; set; }
    }
}
