using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AvansFysioAppInfrastructure.Repos
{
    public class OperationRepo : OperationIRepo
    {
        private MasterDbContext context;
        private DbSet<Operation> operations;


        public OperationRepo(MasterDbContext context)
        {
            this.context = context;
            this.operations = context.Set<Operation>();
        }

        public IEnumerable<Operation> Operations()
        {
            return context.Operations.ToList();
        }

        public Operation GetOperation(string id)
        {
            foreach (var operation in operations)
            {
                if (operation.Value == id)
                {
                    return operation;
                }
            }

            return null;
        }
    }
}
