using System;
using System.Collections.Generic;
using System.Globalization;
using AvansFysioAppDomain.Domain;

namespace AvansFysioAppDomainServices.Validation
{
    public class AppointmentValidation
    {
        public bool availability(DateTime appointmentBegin, DateTime appointmentEnd, DateTime sessionBegin, DateTime sessionEnd)
        {
            bool overlap = (appointmentBegin < sessionEnd && sessionBegin < appointmentEnd);
            if (overlap)
            {
                return true;
            }

            return false;
        }

        public bool PhysioIsAvailable(List<Session> appointmentsOfPhysio, Session session, Physiotherapist physiotherapist)
        {
            bool physiotherapistAvailable = true;
            foreach (Session appointment in appointmentsOfPhysio)
            {
                if ((availability(appointment.AppointmentBegin, appointment.AppointmentEnd, session.AppointmentBegin, session.AppointmentEnd)) || (timeOfPhysio(session.AppointmentBegin, session.AppointmentEnd, physiotherapist.AvailabilityStart, physiotherapist.AvailabilityEnd)))
                {
                    physiotherapistAvailable = false;
                }
            }

            return physiotherapistAvailable;
        }

        public bool timeOfPhysio(DateTime appointmentBegin, DateTime appointmentEnd, DateTime availabilityStart,
            DateTime availabilityEnd)
        {
            if (appointmentBegin.TimeOfDay < availabilityStart.TimeOfDay ||
                appointmentEnd.TimeOfDay > availabilityEnd.TimeOfDay)
            {
                return true;
            }

            return false;
        }

        public bool CheckIfSessionsPerWeekWillBeExceeded(Session session, int max, List<Session> sessionsWithTpId)
        {
            int size = 0;
            var currentCulture = CultureInfo.CurrentCulture;
            var weekNumber1 = currentCulture.Calendar.GetWeekOfYear(
                session.AppointmentBegin,
                currentCulture.DateTimeFormat.CalendarWeekRule,
                currentCulture.DateTimeFormat.FirstDayOfWeek);
            foreach (Session s in sessionsWithTpId)
            {
                var weekNumber2 = currentCulture.Calendar.GetWeekOfYear(
                    s.AppointmentBegin,
                    currentCulture.DateTimeFormat.CalendarWeekRule,
                    currentCulture.DateTimeFormat.FirstDayOfWeek);
                if (weekNumber1 == weekNumber2 && session.AppointmentBegin.Year == s.AppointmentBegin.Year)
                {
                    size++;
                }
            }


            if (size >= max)
            {
                return true;
            }

            return false;
        }

        public bool CheckHoursBeforeAppointmentDeleteIsNotTwentyFour(Session session)
        {
            if (session.AppointmentBegin.AddHours(-24) < DateTime.Now)
            {
                return true;
            }

            return false;
        }
    }

}
