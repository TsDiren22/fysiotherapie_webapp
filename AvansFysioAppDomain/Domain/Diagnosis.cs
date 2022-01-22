using System.ComponentModel.DataAnnotations;

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
