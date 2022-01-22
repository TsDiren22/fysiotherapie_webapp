using System.Collections.Generic;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface TreatmentIRepo
    {
        void AddTreatment(Treatment treatment);
        IEnumerable<Treatment> Treatments();
        Treatment GetTreatment(int id);
        void DeleteTreatmentWithTreatmentId(int id);
        void UpdateTreatment(Treatment treatment);

    }
}
