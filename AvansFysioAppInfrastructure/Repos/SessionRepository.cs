using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;
using AvansFysioAppInfrastructure.Data;

namespace AvansFysioAppInfrastructure.Repos
{
    public class SessionRepository : SessionIRepo
    {
        private DatabaseContext context;

        public SessionRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void AddSession(Session session)
        {
            context.Sessions.Add(session);
            context.SaveChanges();
        }

        public IEnumerable<Session> Sessions()
        {
            return context.Sessions.ToList();
        }

        public Session GetSession(int id)
        {
            return Sessions().FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Session> GetSessionsWithPatientId(int id)
        {
            return context.Sessions.Where(i => i.PatientId == id);
        }

        public IEnumerable<Session> GetSessionsWithTreatmentPlanId(int id)
        {
            return context.Sessions.Where(i => i.TreatmentPlanId == id);
        }
    }
}
