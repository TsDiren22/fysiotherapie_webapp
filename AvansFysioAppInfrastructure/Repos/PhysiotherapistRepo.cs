using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

        public void UpdatePhysiotherapist(Physiotherapist physio)
        {
            context.Physiotherapists.Attach(physio);
            context.Physiotherapists.Remove(physio);
            context.SaveChanges();
        }
    }
}
