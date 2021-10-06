using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysioAppDomain.Domain
{
    public class PatientFile
    {   
        public int Id { get; set; }

        public Patient Patient { get; set; }

        public int? PatientId { get; set; }

        public int Age { get; set; }

        public string DescriptionComplaintsGlobal { get; set; }

        public int DiagnosisNumber { get; set; }

        public Physiotherapist IntakeBy { get; set; }

        public int? IntakeById { get; set; }

        public Physiotherapist SupervisionBy { get; set; }
        public int? SupervisionById { get; set; }

        public Physiotherapist HeadPractitioner { get; set; }

        public int? HeadPractitionerId { get; set; }

        public DateTime DateOfRegister { get; set; }

        public DateTime DateOfEnd { get; set; }

        public string Remark { get; set; }

        public string TreatmentPlan { get; set; }

        public string Treatments { get; set; }

    }
}