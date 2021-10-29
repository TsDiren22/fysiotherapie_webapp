using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface OperationIRepo
    {
        IEnumerable<Operation> Operations();
        Operation GetOperation(string value);
        IEnumerable<Operation> GetOperationByDescription(string description);
        IEnumerable<Operation> GetOperationByMandatory(bool mandatory);
        IEnumerable<Operation> GetOperationByParameters(string description, bool mandatory);
    }
}
