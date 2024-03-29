﻿using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Operation> GetOperationByDescription(string description)
        {
            return Operations().Where(i => i.Description.ToLower().Contains(description.ToLower()));
        }

        public IEnumerable<Operation> GetOperationByMandatory(bool mandatory)
        {
            return Operations().Where(i => i.MandatoryExplanation == mandatory);
        }

        public IEnumerable<Operation> GetOperationByParameters(string description, bool mandatory)
        {
            return Operations().Where(i => i.Description.ToLower().Contains(description.ToLower()) && i.MandatoryExplanation == mandatory);
        }
    }
}
