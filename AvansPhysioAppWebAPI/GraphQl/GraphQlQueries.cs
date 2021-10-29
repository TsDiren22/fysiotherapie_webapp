﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;

namespace AvansPhysioAppWebAPI.GraphQl
{
    public class GraphQlQueries
    {
        private DiagnosisIRepo repository;

        public GraphQlQueries(DiagnosisIRepo repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Diagnosis> Diagnoses => repository.Diagnosis();

        public Diagnosis Diagnosis(string id) => repository.GetDiagnosis(id);
    }
}