using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace AvansFysioAppInfrastructure.Repos
{
    public class TreatmentRepository : TreatmentIRepo
    {
        private DatabaseContext context;

        public TreatmentRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void AddTreatment(Treatment treatment)
        {
            context.Treatments.Add(treatment);
            context.SaveChanges();
        }

        public IEnumerable<Treatment> Treatments()
        {
            return context.Treatments.ToList();
        }

        public Treatment GetTreatment(int id)
        {
            return Treatments().FirstOrDefault(i => i.Id == id);
        }

        public void DeleteTreatmentWithTreatmentId(int id)
        {
            context.Treatments.Remove(GetTreatment(id));
            context.SaveChanges();
        }

        public void UpdateTreatment(Treatment treatment)
        {
            Treatment exist = context.Set<Treatment>().Find(treatment.Id);
            context.Entry(exist).CurrentValues.SetValues(treatment);
            context.SaveChanges();
        }
    }
}
