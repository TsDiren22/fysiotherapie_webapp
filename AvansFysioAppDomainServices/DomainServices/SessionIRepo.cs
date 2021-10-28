using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppInfrastructure.Repos
{
    public interface SessionIRepo
    {
        void AddSession(Session session);
        IEnumerable<Session> Sessions();
        Session GetSession(int id);
        IEnumerable<Session> GetSessionsWithPatientId(int id);
    }
}
