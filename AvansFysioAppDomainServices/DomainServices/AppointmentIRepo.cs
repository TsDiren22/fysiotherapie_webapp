using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.DomainServices
{
    public interface AppointmentIRepo
    {
        void AddAppointment(Appointment appointment);
        IEnumerable<Appointment> Appointments();
        Appointment GetAppointment(int id);
        void UpdateAppointment(Appointment appointment);
        Appointment FindAppointmentWithSessionId(int sessionId);
    }
}
