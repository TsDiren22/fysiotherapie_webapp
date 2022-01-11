using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace AvansFysioApp.Controllers
{
    public class AppointmentController : Controller
    {

        private IRepo repository;
        private PatientFileIRepo fileRepository;
        private IPhysiotherapistRepo physiotherapistRepo;
        private AppointmentIRepo appointmentIRepo;
        private TreatmentPlanIRepo treatmentPlanIRepo;
        private TreatmentIRepo treatmentIRepo;
        private SessionIRepo sessionIRepo;
        private HttpClient client;
        private IConfiguration configuration;
        private UserManager<IdentityUser> userManager;
        private OperationIRepo operationIRepo;
        private IDiagnosisRepo diagnosisRepo;
        private RemarkIRepo remarkIRepo;

        public AppointmentController(IRepo repository, IDiagnosisRepo diagnosisRepo, RemarkIRepo remarkIRepo, PatientFileIRepo fileRepository, IPhysiotherapistRepo physiotherapistRepo, AppointmentIRepo appointmentIRepo, TreatmentPlanIRepo treatmentPlanIRepo, TreatmentIRepo treatmentIRepo, OperationIRepo operationIRepo, SessionIRepo sessionIRepo, IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            this.repository = repository;
            this.fileRepository = fileRepository;
            this.physiotherapistRepo = physiotherapistRepo;
            this.client = new HttpClient()
            {
                BaseAddress = new Uri(configuration.GetConnectionString("BaseUrl"))
            };
            this.remarkIRepo = remarkIRepo;
            this.configuration = configuration;
            this.appointmentIRepo = appointmentIRepo;
            this.operationIRepo = operationIRepo;
            this.sessionIRepo = sessionIRepo;
            this.diagnosisRepo = diagnosisRepo;
            this.treatmentIRepo = treatmentIRepo;
            this.treatmentPlanIRepo = treatmentPlanIRepo;
            this.userManager = userManager;
        }

        public void AddPatientsInViewbag()
        {
            var patients = repository.Patients().Prepend(new Patient() { PatientId = -1, Name = "Select a patient" });
            ViewBag.Patients = new SelectList(patients, "PatientId", "Name");
        }

        public void AddPatientFilesInViewbag()
        {
            var patientFiles = fileRepository.Responses().Prepend(new PatientFile() { Id = -1, Title = "Select a patient" });
            ViewBag.PatientFiles = new SelectList(patientFiles, "Id", "Title");
        }

        public void AddPhysioToList()
        {
            ViewBag.Physio = physiotherapistRepo.Physiotherapists();
        }
        public void AddPatientToList()
        {
            ViewBag.Pat = repository.Patients();
        }

        public void AddPhysiotherapistsInViewbag()
        {
            var physios = physiotherapistRepo.Physiotherapists().Prepend(new Physiotherapist() { Id = -1, Name = "Select a physiotherapist" });
            ViewBag.Physiotherapists = new SelectList(physios, "Id", "Name");
        }

        public async void AddPhysiotherapistsExceptInternInViewbag()
        {

            List<Physiotherapist> noIntern = new List<Physiotherapist>();

            foreach (Physiotherapist p in physiotherapistRepo.Physiotherapists())
            {
                if (!p.IsIntern)
                {
                    noIntern.Add(p);
                }
            }

            var physios = noIntern.Prepend(new Physiotherapist() { Id = -1, Name = "Select a physiotherapist" });
            ViewBag.NoIntern = new SelectList(physios, "Id", "Name");
        }

        public void AddSessionsInViewbag(string email)
        {
            Patient patient = repository.GetPatientByEmail(email);
            Physiotherapist physiotherapist = physiotherapistRepo.getPhysiotherapistByEmail(email);
            List<Session> list = new List<Session>();
            var sessions = list.Prepend(new Session() { Id = -1, Name = "Select a session" });

            if (patient != null)
            {
                foreach (var session in sessionIRepo.Sessions())
                {
                    if (appointmentIRepo.FindAppointmentWithSessionId(session.Id) == null && session.PatientId == patient.PatientId)
                    {
                        list.Add(session);
                    }
                }
            }
            else if (physiotherapist != null)
            {
                foreach (var session in sessionIRepo.Sessions())
                {
                    if (appointmentIRepo.FindAppointmentWithSessionId(session.Id) == null &&
                        session.HeadPhysiotherapistId == physiotherapist.Id)
                    {
                        list.Add(session);
                    }
                }
            }

            ViewBag.SessionList = list;
            ViewBag.Sessions = new SelectList(sessions, "Id", "Name");
        }

        public void AddAppointmentsInViewbag(string email)
        {
            Patient patient = repository.GetPatientByEmail(email);
            Physiotherapist physiotherapist = physiotherapistRepo.getPhysiotherapistByEmail(email);
            List<Appointment> list = new List<Appointment>();

            if (patient != null)
            {
                foreach (var a in appointmentIRepo.Appointments())
                {
                    if (a.PatientId == patient.PatientId)
                    {
                        list.Add(a);
                    }
                }
            }
            else if (physiotherapist != null)
            {
                foreach (var a in appointmentIRepo.Appointments())
                {
                    if (a.HeadPhysiotherapistId == physiotherapist.Id)
                    {
                        list.Add(a);
                    }
                }
            }
            ViewBag.AppointmentList = list;
        }

        public void AddPatientsWithNoPatientFile()
        {
            List<Patient> list = new List<Patient>();
            foreach (var p in repository.Patients())
                if (fileRepository.FindFileWithPatientId(p.PatientId) == null) list.Add(p);
            ViewBag.PatientNoFile = new SelectList(list, "PatientId", "Name");
        }

        public void AddRemarksOfFileToViewBag(int id)
        {
            var list = remarkIRepo.GetRemarksByFile(id);
            ViewBag.Remarks = list;
        }

        public void AddAvailableTimeInViewbag(Physiotherapist physiotherapist, Session session)
        {
            List<DateTime> appointmentStart = new List<DateTime>();
            var start = physiotherapist.AvailabilityStart;
            var end = physiotherapist.AvailabilityEnd;
            var duration = session.Duration;
            while (start <= end)
            {
                if (start.AddMinutes(duration) <= end)
                {
                    appointmentStart.Add(start);
                }
                start = start.AddMinutes(30);
            }


        }


        [Authorize(Policy = "PatientOrPhysioOnly")]
        [HttpGet]
        public async Task<IActionResult> AddAppointment()
        {
            AddPhysioToList();
            AddPatientToList();
            IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);

            string email = user.Email;
            AddSessionsInViewbag(email);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                Session session = sessionIRepo.GetSession((int)appointment.SessionId);
                appointment.AppointmentEnd = appointment.AppointmentBegin.AddMinutes(session.Duration);

                Physiotherapist physiotherapist = physiotherapistRepo.GetPhysiotherapist((int)session.HeadPhysiotherapistId);
                appointment.HeadPhysiotherapist = physiotherapist;

                Patient patient = repository.GetPatient((int)session.PatientId);
                appointment.Patient = patient;
                appointment.AppointmentMade = DateTime.Now;

                appointmentIRepo.AddAppointment(appointment);
                Physiotherapist p = physiotherapistRepo.getPhysiotherapistByEmail(user.Email);
                if (p != null)
                {
                    return RedirectToAction("DetailView", "Patient", new { id = patient.PatientId });
                }
                return RedirectToAction("AppointmentList");

            }

            return View(appointment);
        }

        public async Task<IActionResult> AppointmentList()
        {
            var user = await userManager.GetUserAsync(User);
            Patient p = repository.GetPatientByEmail(user.Email);
            List<Appointment> appointments = new List<Appointment>();
            physiotherapistRepo.Physiotherapists();
            foreach (Appointment a in appointmentIRepo.Appointments())
            {
                if (a.PatientId == p.PatientId)
                {
                    appointments.Add(a);
                }
            }
            return View(appointments);
        }

    }
}
