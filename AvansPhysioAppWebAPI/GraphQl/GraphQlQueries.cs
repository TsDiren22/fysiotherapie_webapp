using System.Collections.Generic;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;

namespace AvansPhysioAppWebAPI.GraphQl
{
    public class GraphQlQueries
    {
        private IDiagnosisRepo repository;

        public GraphQlQueries(IDiagnosisRepo repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Diagnosis> Diagnoses => repository.Diagnosis();

        public Diagnosis Diagnosis(string id) => repository.GetDiagnosis(id);
    }
}
