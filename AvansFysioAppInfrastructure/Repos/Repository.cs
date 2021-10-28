using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AvansFysioAppInfrastructure.Repos
{
    public class Repository : IRepo
    {
        private DatabaseContext context;

        public Repository(DatabaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<Patient> Patients()
        {
            return context.Patients.ToList();
        }

        public Patient GetPatientByEmail(string email)
        {
            return Patients().FirstOrDefault(i => i.Email.Equals(email));
        }

        public void AddPatient(Patient response)
        {
            context.Patients.Add(response);
            context.SaveChanges();
        }

        public int PatientAmount() => context.Patients.Count();

        public Patient GetPatient(int id)
        {
            return Patients().FirstOrDefault(i => i.PatientId == id);
        }

        public void UpdatePatient(Patient patient)
        {
            Patient exist = this.context.Set<Patient>().Find(patient.PatientId);
            this.context.Entry(exist).CurrentValues.SetValues(patient);

            this.context.SaveChanges();
        }

        public IEnumerable<Physiotherapist> Physiotherapists()
        {
            return context.Physiotherapists.ToList();
        }
    }
}
