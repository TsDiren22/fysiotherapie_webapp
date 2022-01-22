using System.Collections.Generic;
using System.Linq;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AvansFysioAppInfrastructure.Repos
{
    public class DiagnosisRepo : IDiagnosisRepo
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

        public IEnumerable<Diagnosis> GetDiagnosesByLocationOnBody(string location)
        {
            return Diagnosis().Where(i => i.LocationOnBody.ToLower().Contains(location.ToLower()));

        }

        public IEnumerable<Diagnosis> GetDiagnosesByPathology(string pathology)
        {
            return Diagnosis().Where(i => i.Pathology.ToLower().Contains(pathology.ToLower()));

        }

        public IEnumerable<Diagnosis> GetDiagnosisByParameters(string location, string pathology)
        {
            return Diagnosis().Where(i => i.LocationOnBody.ToLower().Contains(location.ToLower()) && i.Pathology.ToLower().Contains(pathology.ToLower()));
        }
    }
}
