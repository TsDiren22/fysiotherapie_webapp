using System.Collections.Generic;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface IRepo
    {
        void AddPatient(Patient patient);
        void UpdatePatient(Patient patient);
        int PatientAmount();
        Patient GetPatient(int id);
        IEnumerable<Patient> Patients();
        Patient GetPatientByEmail(string email);
    }
}
