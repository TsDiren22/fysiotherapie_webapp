using System.Collections.Generic;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface SessionIRepo
    {
        void AddSession(Session session);
        void UpdateSession(Session session);
        IEnumerable<Session> Sessions();
        Session GetSession(int id);
        IEnumerable<Session> GetSessionsWithPatientId(int id);
        IEnumerable<Session> GetSessionsWithTreatmentPlanId(int id);
        IEnumerable<Session> GetSessionsWithPhysiotherapistId(int id);
        void DeleteSessionWithSessionId(int id);
    }
}
