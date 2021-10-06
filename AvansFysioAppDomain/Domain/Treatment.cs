using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomain.Domain
{
    public class Treatment
    {
        public int TreatmentId { get; set; }

        public string Type { get; set; }
        public string Description { get; set; }

        public enum Room { PracticeRoom, TreatmentRoom }
        public Room UsedRoom { get; set; }

        public Physiotherapist Physiotherapist { get; set; }

        public DateTime DateOfTreatment { get; set; }
    }
}
