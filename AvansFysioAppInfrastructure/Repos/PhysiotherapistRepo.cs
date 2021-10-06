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
        private DbSet<Physiotherapist> physiotherapist;

        public PhysiotherapistRepo(DatabaseContext context)
        {
            this.context = context;
            this.physiotherapist = context.Set<Physiotherapist>();
        }

        public IEnumerable<Physiotherapist> physiotherapists()
        {
            return context.Physiotherapists.ToList();
        }

        public Physiotherapist getPhysiotherapist(int id)
        {
            return physiotherapists().FirstOrDefault(i => i.Id == id);
        }
    }
}
