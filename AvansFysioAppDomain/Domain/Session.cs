using System;
using System.ComponentModel.DataAnnotations;
using AvansFysioAppDomain.Domain.DataAnnotations;

namespace AvansFysioAppDomain.Domain
{
    public class Session
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public int? PatientId { get; set; }
        public TreatmentPlan TreatmentPlan { get; set; }
        public int TreatmentPlanId { get; set; }
        public Physiotherapist HeadPhysiotherapist { get; set; }
        public int? HeadPhysiotherapistId { get; set; }
        public DateTime AppointmentMade { get; set; }
        [Required(ErrorMessage = "Time of appointment is required!")]
        [DateAnnotations(ErrorMessage = "An appointment must be set in the future")]
        public DateTime AppointmentBegin { get; set; }
        public DateTime AppointmentEnd { get; set; }
    }
}
