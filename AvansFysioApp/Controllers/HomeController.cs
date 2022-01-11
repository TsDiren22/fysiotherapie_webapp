using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using AvansFysioApp.ExtentionMethods;
using AvansFysioApp.Models;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private OperationIRepo operationIRepo;
        private IDiagnosisRepo diagnosisRepo;
        private RemarkIRepo remarkIRepo;

        public HomeController(IRepo repository, IDiagnosisRepo diagnosisRepo, RemarkIRepo remarkIRepo, PatientFileIRepo fileRepository, IPhysiotherapistRepo physiotherapistRepo, AppointmentIRepo appointmentIRepo, TreatmentPlanIRepo treatmentPlanIRepo, TreatmentIRepo treatmentIRepo, OperationIRepo operationIRepo, SessionIRepo sessionIRepo, IConfiguration configuration, UserManager<IdentityUser> userManager)
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

        public IActionResult Index()
        {
            AddPhysioToList();  
            AddPatientToList();
            return View(repository.Patients());
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
            ViewBag.Pat= repository.Patients();
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

        private IEnumerable<Operation> GetOperationAsync(string endpoint = "Operation")
        {
            var response = client.GetAsync(endpoint);
            var result = response.Result;
            IEnumerable<Operation> codes;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<List<Operation>>();
                data.Wait();
                codes = data.Result;
            }
            else codes = Enumerable.Empty<Operation>();
            return codes;
        }

        [Authorize(Policy = "InternOrPhysioOnly")]
        [HttpGet]
        public IActionResult FindDiagnosis(string searchString = "", string empty = "")
        {
            AddPhysioToList();
            AddPatientToList();
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

        [Authorize(Policy = "InternOrPhysioOnly")]
        [HttpPost]
        public IActionResult FindDiagnosis(string id)
        {
            AddPhysiotherapistsInViewbag();
            AddPatientsInViewbag();
            PatientFile patientFile = TempData.Get<PatientFile>("patientFile") ?? TempData.Get<PatientFile>("file");
            Patient patient = repository.GetPatient((int)patientFile.PatientId);
            patientFile.DiagnosisNumber = id;
            Diagnosis d = diagnosisRepo.GetDiagnosis(id);
            patientFile.DiagnosisDescription = d.Pathology;
            var file = fileRepository.FindFileWithPatientId(patient.PatientId);
            if (file != null)
            {
                fileRepository.UpdatePatientFile(patientFile);
            }
            else
            {
                fileRepository.AddPatientFile(patientFile);
            }
            return RedirectToAction("DetailView", "Patient", new { id = patientFile.PatientId });
        }
        
        [HttpGet]
        public IActionResult FindOperation(string searchString = "", string empty = "")
        {
            AddPhysioToList();
            AddPatientToList();
            IEnumerable<Operation> list;
            if (searchString != null)
            {
                var endpoint = "Operation";
                endpoint = QueryHelpers.AddQueryString(endpoint, "Description", searchString);
                list = GetOperationAsync(endpoint);
            }
            else list = GetOperationAsync();
            
            return View(list);
        }
        
        [HttpPost]
        public IActionResult FindOperation(string id)
        {
            Operation operation = operationIRepo.GetOperation(id);
            TempData.Put("operation", operation.Value);

            return RedirectToAction("AddTreatment", "Patientfile");
        }
    }
}