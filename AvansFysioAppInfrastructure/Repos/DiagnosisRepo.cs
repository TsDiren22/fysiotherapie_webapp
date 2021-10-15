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
    public class DiagnosisRepo : DiagnosisIRepo
    {
        private MasterDbContext context;
        private DbSet<Diagnosis> diagnoses;


        public DiagnosisRepo(MasterDbContext context)
        {
            this.context = context;
            this.diagnoses = context.Set<Diagnosis>();
        }

        public IEnumerable<Diagnosis> Diagnosis()
        {
            return context.Diagnoses.ToList();
        }

        public Diagnosis GetDiagnosis(string id)
        {
            foreach (var diagnosis in diagnoses)
            {
                if (diagnosis.Code == id)
                {
                    return diagnosis;
                }
            }

            return null;
        }
    }
}
