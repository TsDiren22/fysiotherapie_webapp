using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFysioAppDomain.Domain
{
    public class TreatmentPlan
    {
        public int Id { get; set; }
        public int MaxSessions { get; set; }
        public List<Session> AllSession { get; set; }
    }
}
