using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AvansFysioApp.ExtentionMethods;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppInfrastructure.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace AvansFysioApp.Controllers
{
    public class PatientfileController : Controller
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

        public PatientfileController(IRepo repository, IDiagnosisRepo diagnosisRepo, RemarkIRepo remarkIRepo, PatientFileIRepo fileRepository, IPhysiotherapistRepo physiotherapistRepo, AppointmentIRepo appointmentIRepo, TreatmentPlanIRepo treatmentPlanIRepo, TreatmentIRepo treatmentIRepo, OperationIRepo operationIRepo, SessionIRepo sessionIRepo, IConfiguration configuration, UserManager<IdentityUser> userManager)
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

        [Authorize(Policy = "InternOrPhysioOnly")]
        [HttpGet]
        public ActionResult AddFileView()
        {
            AddPhysioToList();
            AddPatientToList();
            AddPhysiotherapistsInViewbag();
            AddPatientsWithNoPatientFile();
            AddPhysiotherapistsExceptInternInViewbag();
            return View();
        }

        [Authorize(Policy = "InternOrPhysioOnly")]
        [HttpPost]
        public async Task<ActionResult> AddFileView(PatientFile patientFile)
        {
            if (ModelState.IsValid)
            {
                if (patientFile.SupervisionById == -1)
                {
                    ModelState.AddModelError("", "Please select a supervisor.");
                    AddPhysioToList();
                    AddPatientToList();
                    AddPatientsWithNoPatientFile();
                    AddPhysiotherapistsInViewbag();
                    AddPhysiotherapistsExceptInternInViewbag();
                    return View(patientFile);
                }
                if (patientFile.PatientId != null)
                {
                    Patient patient = repository.GetPatient((int)patientFile.PatientId);
                    patientFile.Age = patient.GetAge();
                    patientFile.Title = patient.Name;
                }

                var user = await userManager.GetUserAsync(User);
                Physiotherapist p = physiotherapistRepo.getPhysiotherapistByEmail(user.Email);
                patientFile.IntakeById = p.Id;
                
                repository.Patients();
                TempData.Put("patientFile", patientFile);
                return RedirectToAction("FindDiagnosis", "Home");

            }
            AddPhysioToList();
            AddPatientToList();
            AddPhysiotherapistsInViewbag();
            AddPhysiotherapistsExceptInternInViewbag();
            return View(patientFile);
        }

        [Authorize(Policy = "InternOrPhysioOnly")]
        [HttpGet]
        public ActionResult UpdateFileView(int id)
        {
            AddPhysioToList();
            AddPatientToList();
            AddPatientsInViewbag();
            AddPhysiotherapistsInViewbag();
            AddPhysiotherapistsExceptInternInViewbag();

            PatientFile patientFile = fileRepository.GetPatientFile(id);
            return View(patientFile);
        }

        [Authorize(Policy = "InternOrPhysioOnly")]
        [HttpPost]
        public ActionResult UpdateFileView(PatientFile patientFile)
        {
            if (ModelState.IsValid)
            {
                if (patientFile.SupervisionById == -1)
                {
                    patientFile.SupervisionById = null;
                }
                repository.Patients();
                fileRepository.UpdatePatientFile(patientFile);
                return RedirectToAction("DetailView", "Patient", new { id = patientFile.PatientId });
            }
            else return View(patientFile);
        }


        [HttpGet]
        public IActionResult AddTreatment()
        {
            AddPhysiotherapistsInViewbag();
            AddPatientToList();
            AddPatientFilesInViewbag();
            return View();
        }

        [HttpPost]
        public IActionResult AddTreatment(Treatment treatment)
        {
            AddPhysiotherapistsInViewbag();
            AddPatientToList();
            AddPatientFilesInViewbag();
            if (ModelState.IsValid)
            {
                Operation operation = operationIRepo.GetOperation(TempData.Get<string>("operation"));

                //var treatments = fileRepository.FindTreatmentswithPatientfileId(treatment.PatientFileId);

                if ((operation.MandatoryExplanation && treatment.Particularities == null))
                {
                    if (operation.MandatoryExplanation && treatment.Particularities == null)
                    {
                        ModelState.AddModelError("", "Particularities are mandatory with this treatment!");
                    }
                    /*
                    if (t != null)
                    {
                        ModelState.AddModelError("", "This treatment is already added. Please try another one!");
                    }*/
                    TempData.Put("operation", operation.Value);
                    return View(treatment);
                }

                treatment.Description = operation.Description;
                treatment.Type = operation;

                PatientFile patientFile = fileRepository.GetPatientFile(treatment.PatientFileId);
                treatment.PatientFile = patientFile;

                treatmentIRepo.AddTreatment(treatment);
                return RedirectToAction("DetailView", "Patient", new { id = treatment.PatientFileId });
            }
            return View(treatment);
        }


        [Authorize(Policy = "InternOrPhysioOnly")]
        [HttpGet]
        public IActionResult AddSession()
        {
            AddPhysioToList();
            AddPatientToList();
            AddPatientsInViewbag();
            return View();
        }

        [Authorize(Policy = "InternOrPhysioOnly")]
        [HttpPost]
        public async Task<IActionResult> AddSession(Session session)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.GetUserAsync(User);
                string email = user.Email;

                Physiotherapist physiotherapist = physiotherapistRepo.getPhysiotherapistByEmail(email);
                Patient patient = repository.GetPatient((int)session.PatientId);
                PatientFile patientFile = fileRepository.FindFileWithPatientId(patient.PatientId);
                TreatmentPlan treatmentPlan = treatmentPlanIRepo.FindTreatmentPlanWithPatientFile(patientFile.Id);
                int size = sessionIRepo.GetSessionsWithTreatmentPlanId(treatmentPlan.Id).Count();
                if (size >= treatmentPlan.MaxSessions)
                {
                    ModelState.AddModelError("", "The maximum amount of sessions this week have been reached.");
                    AddPhysioToList();
                    AddPatientToList();
                    AddPatientsInViewbag();
                    return View(session);
                }

                session.HeadPhysiotherapist = physiotherapist;
                session.TreatmentPlanId = treatmentPlan.Id;
                sessionIRepo.AddSession(session);

                return RedirectToAction("DetailView", "Patient", new { id = session.PatientId });
            }
            return View();
        }


        [HttpGet]
        public IActionResult AddRemark()
        {
            AddPatientFilesInViewbag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRemark(Remark remark)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                Physiotherapist physio = physiotherapistRepo.getPhysiotherapistByEmail(user.Email);
                PatientFile file = fileRepository.GetPatientFile(remark.FileByRemarkId);
                var list = remarkIRepo.GetRemarksByFile(file.Id);
                remark.Date = DateTime.Now;
                remark.RemarkMadeById = physio.Id;
                list.Add(remark);
                file.Remarks = list;
                fileRepository.UpdatePatientFile(file);
            }

            return View();
        }

        [HttpGet]
        public IActionResult AddTreatmentPlan()
        {
            AddPatientFilesInViewbag();
            return View();
        }

        [HttpPost]
        public IActionResult AddTreatmentPlan(TreatmentPlan treatment)
        {
            if (ModelState.IsValid)
            {
                PatientFile patientFile = fileRepository.GetPatientFile(treatment.FileId);
                treatmentPlanIRepo.AddTreatmentPlan(treatment);
                return RedirectToAction("DetailView", "Patient", new { id = patientFile.PatientId });
            }
            AddPatientFilesInViewbag();
            return View(treatment);
        }
    }
}
