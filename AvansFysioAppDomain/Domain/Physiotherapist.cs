using System;

namespace AvansFysioAppDomain.Domain
{
    public class Physiotherapist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int EmployeeId { get; set; }

        public int? BigId { get; set; }
        public bool IsIntern { get; set; }
        public DateTime AvailabilityStart { get; set; }
        public DateTime AvailabilityEnd { get; set; }
    }
}
