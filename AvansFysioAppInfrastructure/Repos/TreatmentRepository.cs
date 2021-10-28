using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;

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
    }
}
