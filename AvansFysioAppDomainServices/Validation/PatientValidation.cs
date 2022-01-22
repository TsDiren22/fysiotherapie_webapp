using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.Validation
{
    public class PatientValidation
    {
        public bool ReturnTrueWhenParticularitiesIsEmptyWithMandatoryExplanationTrue(Operation operation, Treatment treatment)
        {
            if (operation.MandatoryExplanation && treatment.Particularities == null)
            {
                return true;
            }

            return false;
        }

        public bool ReturnTrueWhenDateIsOutsideRegistration(PatientFile patientFile, Treatment treatment)
        {
            if (treatment.DateOfTreatment > patientFile.DateOfEnd || treatment.DateOfTreatment < patientFile.DateOfRegister)
            {
                return true;
            }

            return false;
        }
    }
}
