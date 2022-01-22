using System.Collections.Generic;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface PatientFileIRepo
    {
        void AddPatientFile(PatientFile patientFile);
        void UpdatePatientFile(PatientFile patientFile);
        PatientFile GetPatientFile(int id);
        IEnumerable<PatientFile> Responses();
        PatientFile FindFileWithPatientId(int id);
        List<Treatment> FindTreatmentswithPatientfileId(int id);
    }
}
