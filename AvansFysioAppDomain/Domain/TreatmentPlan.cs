using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFysioAppDomain.Domain
{
    public class TreatmentPlan
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "Maximum amount of sessions is required!")]
        public int MaxSessions { get; set; }
        [Required(ErrorMessage = "Duration of sessions are required!")]
        public int Duration { get; set; }
        public PatientFile File { get; set; }
        public int FileId { get; set; }
        public Physiotherapist Physiotherapist { get; set; }
        public int PhysiotherapistId { get; set; }
    }
}
