﻿using System;

namespace AvansFysioAppDomain.Domain
{
    public class Treatment
    {
        public int Id { get; set; }
        public string TypeId { get; set; }
        public Operation Type { get; set; }
        public string Description { get; set; }
        public string Room { get; set; }
        public Physiotherapist Physiotherapist { get; set; }
        public int? PhysiotherapistId { get; set; }
        public DateTime DateOfTreatment { get; set; }
        public DateTime DateCreated { get; set; }
        public PatientFile PatientFile { get; set; }
        public int PatientFileId { get; set; }
        public string Particularities { get; set; }
    }
}
