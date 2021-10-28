using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AvansFysioApp.ExtentionMethods;
using AvansFysioApp.Models;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace AvansFysioApp.Controllers
{
    public class HomeController : Controller
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

        public HomeController(IRepo repository, PatientFileIRepo fileRepository, IPhysiotherapistRepo physiotherapistRepo, AppointmentIRepo appointmentIRepo, TreatmentPlanIRepo treatmentPlanIRepo, TreatmentIRepo treatmentIRepo, SessionIRepo sessionIRepo, IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            this.repository = repository;
            this.fileRepository = fileRepository;
            this.physiotherapistRepo = physiotherapistRepo;
            this.client = new HttpClient()
            {
                BaseAddress = new Uri(configuration.GetConnectionString("BaseUrl"))
            };
            this.configuration = configuration;
            this.appointmentIRepo = appointmentIRepo;
            this.sessionIRepo = sessionIRepo;
            this.treatmentIRepo = treatmentIRepo;
            this.treatmentPlanIRepo = treatmentPlanIRepo;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(repository.Patients());
        }

        [Authorize(Policy = "PhysiotherapistOnly")]
        [HttpGet]
        public ActionResult AddPatientView()
        {
            return View();
        }

        [Authorize(Policy = "PhysiotherapistOnly")]
        [HttpPost]
        public ActionResult AddPatientView(Patient patient)
        {
            if (ModelState.IsValid)
            {
                repository.AddPatient(patient);
                return RedirectToAction("DetailView", new { id = patient.PatientId });
            }
            else return View(patient);
            }

        [Authorize(Policy = "PhysiotherapistOnly")]
        [HttpGet]
        public ActionResult EditPatientView(int id)
        {
            Patient patient = repository.GetPatient(id);
            return View(patient);
        }

        [Authorize(Policy = "PhysiotherapistOnly")]
        [HttpPost]
        public ActionResult EditPatientView(Patient patient)
        {
            if (ModelState.IsValid)
            {
                repository.UpdatePatient(patient);
                return RedirectToAction("DetailView", new {id = patient.PatientId});
            }
            else return View(patient);
        }
        
        public IActionResult PatientList()
        {
            return View("PatientList", repository.Patients());
        }

        [HttpGet]
        public ActionResult DetailView(int id)
        {
            Patient patient = repository.GetPatient(id);
            PatientFile patientFile = fileRepository.FindFileWithPatientId(id);

            physiotherapistRepo.Physiotherapists();

            List<PatientFile> pf = new List<PatientFile>();
            pf.Add(patientFile);
            ViewBag.PatientFile = pf;
            return View("DetailView", patient);
        }

        public void AddPatientsInViewbag()
        {
            var patients = repository.Patients().Prepend(new Patient() { PatientId = -1, Name = "Select a patient" });
            ViewBag.Patients = new SelectList(patients, "PatientId", "Name");
        }

        public void AddPhysiotherapistsInViewbag()
        {
            var physios = physiotherapistRepo.Physiotherapists().Prepend(new Physiotherapist() { Id = -1, Name = "Select a physiotherapist" });
            ViewBag.Physiotherapists = new SelectList(physios, "Id", "Name");
        }
        
        public void AddSessionsInViewbag(string email)
        {
            Patient patient = repository.GetPatientByEmail(email);
            List<Session> list = new List<Session>();
            var sessions = list.Prepend(new Session() { Id = -1, Name = "Select a session" });
            foreach (var session in sessionIRepo.Sessions())
            {
                if (appointmentIRepo.FindAppointmentWithSessionId(session.Id) == null && session.PatientId == patient.PatientId)
                {
                    list.Add(session);
                }
            }

            ViewBag.Sessions = new SelectList(sessions, "Id", "Name");
        }

        public void AddPatientsWithNoPatientFile()
        {
            List<Patient> list = new List<Patient>();
            foreach (var p in repository.Patients()) 
                if (fileRepository.FindFileWithPatientId(p.PatientId) == null) list.Add(p);
            ViewBag.PatientNoFile = new SelectList(list, "PatientId", "Name");
        }

        [HttpGet]
        public ActionResult AddFileView()
        {
            AddPhysiotherapistsInViewbag();
            AddPatientsWithNoPatientFile();
            return View();
        }

        [HttpPost]
        public ActionResult AddFileView(PatientFile patientFile)
        {
            if (ModelState.IsValid)
            {
                if (patientFile.PatientId != null)
                {
                    Patient patient = repository.GetPatient((int)patientFile.PatientId);
                    patientFile.Age = patient.GetAge();
                }
                
                TempData.Put("patientFile", patientFile);

                repository.Patients();
                TempData.Put("patientFile", patientFile);
                return RedirectToAction("FindDiagnosis");

            }
            else return View(patientFile);
            }

        [HttpGet]
        public ActionResult UpdateFileView(int id)
        {
            AddPatientsInViewbag();
            AddPhysiotherapistsInViewbag();
            PatientFile patientFile = fileRepository.GetPatientFile(id);
            return View(patientFile);
        }

        [HttpPost]
        public ActionResult UpdateFileView(PatientFile patientFile)
        {
            if (ModelState.IsValid)
            {
                fileRepository.UpdatePatientFile(patientFile);
                return RedirectToAction("DetailView", new { id = patientFile.PatientId });
            }
            else return View(patientFile);
        }

        private IEnumerable<Diagnosis> GetDiagnosisAsync(string endpoint = "Diagnosis")
        {
            var response = client.GetAsync(endpoint);
            var result = response.Result;
            IEnumerable<Diagnosis> codes;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<List<Diagnosis>>();
                data.Wait();
                codes = data.Result;
            }
            else codes = Enumerable.Empty<Diagnosis>();
            return codes;
        }

        [HttpGet]
        public IActionResult FindDiagnosis(string searchString = "", string empty = "")
        {
            IEnumerable<Diagnosis> list;
            if (searchString != null)
            {
                var endpoint = "Diagnosis";
                endpoint = QueryHelpers.AddQueryString(endpoint, "LocationOnBody", searchString);
                list = GetDiagnosisAsync(endpoint);
            }
            else list = GetDiagnosisAsync();
            
            return View(list);
        }

        [HttpPost]
        public IActionResult FindDiagnosis(string id)
        {
            AddPhysiotherapistsInViewbag();
            AddPatientsInViewbag();
            PatientFile patientFile = TempData.Get<PatientFile>("patientFile");
            patientFile.DiagnosisNumber = id;
            fileRepository.AddPatientFile(patientFile);
            return RedirectToAction("DetailView", new { id = patientFile.PatientId });
        }

        [HttpGet]
        public IActionResult AddSession()
        {
            AddPatientsInViewbag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSession(Session session)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);
                string email = user.Email;

                Physiotherapist physiotherapist = physiotherapistRepo.getPhysiotherapistByEmail(email);
                session.HeadPhysiotherapist = physiotherapist;
                sessionIRepo.AddSession(session);
                RedirectToAction("DetailView", new { id = session.PatientId });
            }

            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddAppointment()
        {
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
                Session session = sessionIRepo.GetSession((int)appointment.SessionId);
                appointment.AppointmentEnd = appointment.AppointmentBegin.AddMinutes(session.Duration);

                Physiotherapist physiotherapist = physiotherapistRepo.GetPhysiotherapist((int)session.HeadPhysiotherapistId);
                appointment.HeadPhysiotherapist = physiotherapist;

                IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);

                string email = user.Email;
                appointment.Patient = repository.GetPatientByEmail(email);
                appointmentIRepo.AddAppointment(appointment);
                return RedirectToAction();
            }

            return View("Index");
        }
    }
}