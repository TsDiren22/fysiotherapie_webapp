using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AvansFysioAppInfrastructure.Repos
{
    public class PatientFileRepository : PatientFileIRepo
    {
        private DatabaseContext context;
        private DbSet<PatientFile> patientFiles;

        public PatientFileRepository(DatabaseContext context)
        {
            this.context = context;
            this.patientFiles = context.Set<PatientFile>();
        }

        public IEnumerable<PatientFile> Responses()
        {
            return context.PatientFiles.ToList();
        }

        public PatientFile FindFileWithPatientId(int id)
        {
            foreach (var patientFile in patientFiles)
            {
                if (patientFile.PatientId == id)
                {
                    return patientFile;
                }
            }

            return null;
        }

        public List<Treatment> FindTreatmentswithPatientfileId(int id)
        {
            return patientFiles.FirstOrDefault(x => x.Id == id)?.Treatments;
        }

        public void AddPatientFile(PatientFile patientFile)
        {
            context.PatientFiles.Add(patientFile);
            context.SaveChanges();
        }

        public PatientFile GetPatientFile(int id)
        {
            return Responses().FirstOrDefault(i => i.Id == id);
        }

        public void UpdatePatientFile(PatientFile patientFile)
        {
            PatientFile exist = this.patientFiles.Find(patientFile.Id);
            this.context.Entry(exist).CurrentValues.SetValues(patientFile);

            this.context.SaveChanges();
        }
    }
}
