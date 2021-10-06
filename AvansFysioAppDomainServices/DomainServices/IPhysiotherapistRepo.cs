using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface IPhysiotherapistRepo
    {
        IEnumerable<Physiotherapist> physiotherapists();
        Physiotherapist getPhysiotherapist(int id);
    }
}
