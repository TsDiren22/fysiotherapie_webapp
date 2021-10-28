using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFysioAppDomain.Domain
{
    public class Appointment
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public int? PatientId { get; set; }
        public Physiotherapist HeadPhysiotherapist { get; set; }
        public int? HeadPhysiotherapistId { get; set; }
        public DateTime AppointmentBegin { get; set; }
        public DateTime AppointmentEnd { get; set; }
        public Session Session { get; set; }
        public int? SessionId { get; set; }
    }
}
