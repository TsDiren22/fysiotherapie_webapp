using System.Collections.Generic;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface IPhysiotherapistRepo
    {
        IEnumerable<Physiotherapist> Physiotherapists();
        Physiotherapist GetPhysiotherapist(int id);

        Physiotherapist getPhysiotherapistByEmail(string email);
    }
}
