﻿using System.Collections.Generic;
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
