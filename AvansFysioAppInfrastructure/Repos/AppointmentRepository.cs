using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Data;

namespace AvansFysioAppInfrastructure.Repos
{
    public class AppointmentRepository : AppointmentIRepo
    {
        private DatabaseContext context;

        public AppointmentRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void AddAppointment(Appointment appointment)
        {
            context.Appointments.Add(appointment);
            context.SaveChanges();
        }

        public IEnumerable<Appointment> Appointments()
        {
            return context.Appointments.ToList();
        }

        public Appointment GetAppointment(int id)
        {
            return Appointments().FirstOrDefault(i => i.Id == id);
        }

        public void UpdateAppointment(Appointment appointment)
        {
            Appointment exist = this.context.Set<Appointment>().Find(appointment.Id);
            this.context.Entry(exist).CurrentValues.SetValues(appointment);

            this.context.SaveChanges();
        }

        public Appointment FindAppointmentWithSessionId(int sessionId)
        {
            return Appointments().FirstOrDefault(i => i.SessionId == sessionId);
        }
    }
}
