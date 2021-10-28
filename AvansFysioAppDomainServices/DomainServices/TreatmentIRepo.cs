using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface TreatmentIRepo
    {
        void AddTreatment(Treatment treatment);
        IEnumerable<Treatment> Treatments();
        Treatment GetTreatment(int id);
    }
}
