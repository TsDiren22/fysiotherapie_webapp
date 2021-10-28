using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface TreatmentPlanIRepo
    {
        void AddTreatmentPlan(TreatmentPlan treatmentPlan);
        IEnumerable<TreatmentPlan> TreatmentPlans();
        TreatmentPlan GetTreatmentPlan(int id);
        void UpdateTreatmentPlan(TreatmentPlan treatmentPlan);
    }
}
