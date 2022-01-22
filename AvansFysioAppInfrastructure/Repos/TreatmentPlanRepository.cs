using System.Collections.Generic;
using System.Linq;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;

namespace AvansFysioAppInfrastructure.Repos
{
    public class TreatmentPlanRepository : TreatmentPlanIRepo
    {
        private DatabaseContext context;

        public TreatmentPlanRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void AddTreatmentPlan(TreatmentPlan treatmentPlan)
        {
            context.TreatmentPlans.Add(treatmentPlan);
            context.SaveChanges();
        }

        public IEnumerable<TreatmentPlan> TreatmentPlans()
        {
            return context.TreatmentPlans.ToList();
        }

        public TreatmentPlan GetTreatmentPlan(int id)
        {
            return TreatmentPlans().FirstOrDefault((i => i.Id == id));
        }

        public void UpdateTreatmentPlan(TreatmentPlan treatmentPlan)
        {
            TreatmentPlan exist = this.context.Set<TreatmentPlan>().Find(treatmentPlan.Id);
            this.context.Entry(exist).CurrentValues.SetValues(treatmentPlan);

            this.context.SaveChanges(); 
        }

        public TreatmentPlan FindTreatmentPlanWithPatientFile(int id)
        {
            return TreatmentPlans().FirstOrDefault(x => x.FileId == id);
        }

        public List<TreatmentPlan> findTreatmentPlanWithPhysiotherapistId(int id)
        {
            return TreatmentPlans().Where(x => x.PhysiotherapistId == id).ToList();
        }
    }
}
