using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AvansFysioAppDomain.Domain
{
    public class PatientFile
    {   
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public string Title { get; set; }
        public int? PatientId { get; set; }
        public int Age { get; set; }
        [Required(ErrorMessage = "Please fill in the description of the complaints!")]
        public string DescriptionComplaintsGlobal { get; set; }
        public string DiagnosisNumber { get; set; }
        public string DiagnosisDescription { get; set; }
        public Physiotherapist IntakeBy { get; set; }
        public int? IntakeById { get; set; }
        public Physiotherapist SupervisionBy { get; set; }
        public int? SupervisionById { get; set; }
        public Physiotherapist HeadPractitioner { get; set; }
        public int? HeadPractitionerId { get; set; }
        [Required(ErrorMessage = "Date of registration is required!")]
        public DateTime DateOfRegister { get; set; }
        public DateTime? DateOfEnd { get; set; }
        public List<Remark> Remarks { get; set; }
        public List<Treatment> Treatments { get; set; }
    }
}