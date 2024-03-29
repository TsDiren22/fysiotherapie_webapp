﻿using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Session> GetSessionsWithPhysiotherapistId(int id)
        {
            return context.Sessions.Where(i => i.HeadPhysiotherapistId == id);
        }
        public void UpdateSession(Session session)
        {
            Session exist = this.context.Set<Session>().Find(session.Id);
            this.context.Entry(exist).CurrentValues.SetValues(session);
            this.context.SaveChanges();
        }


        public IEnumerable<Session> GetSessionsWithTreatmentPlanId(int id)
        {
            return context.Sessions.Where(i => i.TreatmentPlanId == id);
        }

        public void DeleteSessionWithSessionId(int id)
        {
            context.Sessions.Remove(GetSession(id));
            context.SaveChanges();
        }
    }
}
