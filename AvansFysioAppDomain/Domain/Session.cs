﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFysioAppDomain.Domain
{
    public class Session
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Patient Patient { get; set; }
        public int? PatientId { get; set; }
        public int Duration { get; set; }
        public Physiotherapist HeadPhysiotherapist { get; set; }
        public int? HeadPhysiotherapistId { get; set; }
    }
}