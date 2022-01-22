using System.Collections.Generic;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface IDiagnosisRepo
    {
        IEnumerable<Diagnosis> Diagnosis();
        Diagnosis GetDiagnosis(string id);
        IEnumerable<Diagnosis> GetDiagnosesByLocationOnBody(string location);
        IEnumerable<Diagnosis> GetDiagnosesByPathology(string pathology);
        IEnumerable<Diagnosis> GetDiagnosisByParameters(string location, string pathology);
    }
}
