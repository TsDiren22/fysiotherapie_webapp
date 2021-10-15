using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface IPhysiotherapistRepo
    {
        IEnumerable<Physiotherapist> Physiotherapists();
        Physiotherapist GetPhysiotherapist(int id);
        void UpdatePhysiotherapist(Physiotherapist physiotherapist);
    }
}
