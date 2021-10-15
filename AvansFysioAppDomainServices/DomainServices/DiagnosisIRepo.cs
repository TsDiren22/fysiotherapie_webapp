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
    }
}
