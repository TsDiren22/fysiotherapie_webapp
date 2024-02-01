using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace AvansFysioAppInfrastructure.Repos
{
    public class PhysiotherapistRepo : IPhysiotherapistRepo
    {

        private DatabaseContext context;

        public PhysiotherapistRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<Physiotherapist> Physiotherapists()
        {
            return context.Physiotherapists.ToList();
        }

        public Physiotherapist GetPhysiotherapist(int id)
        {
            return Physiotherapists().FirstOrDefault(i => i.Id == id);
        }

        public Physiotherapist getPhysiotherapistByEmail(string email)
        {
            return Physiotherapists().FirstOrDefault(i => i.Email.Equals(email));
        }
    }
}
