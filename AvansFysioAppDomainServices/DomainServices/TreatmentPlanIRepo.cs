using System.Collections.Generic;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface TreatmentPlanIRepo
    {
        void AddTreatmentPlan(TreatmentPlan treatmentPlan);
        IEnumerable<TreatmentPlan> TreatmentPlans();
        TreatmentPlan GetTreatmentPlan(int id);
        void UpdateTreatmentPlan(TreatmentPlan treatmentPlan);
        TreatmentPlan FindTreatmentPlanWithPatientFile(int id);
        List<TreatmentPlan> findTreatmentPlanWithPhysiotherapistId(int id);
    }
}
