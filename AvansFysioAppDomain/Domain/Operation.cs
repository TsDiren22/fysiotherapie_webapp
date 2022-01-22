using System.ComponentModel.DataAnnotations;

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
