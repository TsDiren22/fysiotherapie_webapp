using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface DiagnosisIRepo
    {
        IEnumerable<Diagnosis> Diagnosis();
        Diagnosis GetDiagnosis(string id);
        IEnumerable<Diagnosis> GetDiagnosesByLocationOnBody(string location);
        IEnumerable<Diagnosis> GetDiagnosesByPathology(string pathology);
        IEnumerable<Diagnosis> GetDiagnosisByParameters(string location, string pathology);
    }
}
