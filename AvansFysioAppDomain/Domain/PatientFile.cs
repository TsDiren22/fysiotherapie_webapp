using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysioAppDomain.Domain
{
    public class PatientFile
    {   
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public string Title { get; set; }
        public int? PatientId { get; set; }
        public int Age { get; set; }
        //Required(ErrorMessage = "Please fill in the description of the complaints!")]
        public string DescriptionComplaintsGlobal { get; set; }
        public string DiagnosisNumber { get; set; }
        public Physiotherapist IntakeBy { get; set; }
        public int? IntakeById { get; set; }
        public Physiotherapist SupervisionBy { get; set; }
        public int? SupervisionById { get; set; }
        public Physiotherapist HeadPractitioner { get; set; }
        public int? HeadPractitionerId { get; set; }
        //[Required(ErrorMessage = "Please fill in the date of registration!")]
        public DateTime DateOfRegister { get; set; }
        public DateTime DateOfEnd { get; set; }
        //[Required(ErrorMessage = "Please fill in the remark!")]
        public string Remark { get; set; }
        //[Required(ErrorMessage = "Please fill in the treatment plan!")]
        public TreatmentPlan TreatmentPlan { get; set; }
        //[Required(ErrorMessage = "Please fill in the treatments")]
        public List<Treatment> Treatments { get; set; }
    }
}