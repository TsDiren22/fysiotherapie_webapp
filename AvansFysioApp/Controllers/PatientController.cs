using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class PatientController : Controller
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

        public PatientController(IRepo repository, IDiagnosisRepo diagnosisRepo, RemarkIRepo remarkIRepo, PatientFileIRepo fileRepository, IPhysiotherapistRepo physiotherapistRepo, AppointmentIRepo appointmentIRepo, TreatmentPlanIRepo treatmentPlanIRepo, TreatmentIRepo treatmentIRepo, OperationIRepo operationIRepo, SessionIRepo sessionIRepo, IConfiguration configuration, UserManager<IdentityUser> userManager)
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
        public ActionResult AddPatientView()
        {
            return View();
        }

        [Authorize(Policy = "InternOrPhysioOnly")]
        [HttpPost]
        public ActionResult AddPatientView(PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                if (repository.GetPatientByEmail(patientViewModel.Email) != null)
                {
                    ModelState.AddModelError("", "Email is already in use.");
                    return View();
                }

                var patient = new Patient
                {
                    Name = patientViewModel.Name,
                    Email = patientViewModel.Email,
                    Phone = patientViewModel.Phone,
                    Occupation = patientViewModel.Occupation,
                    OccupationId = patientViewModel.OccupationId,
                    Birthday = patientViewModel.Birthday,
                    Gender = patientViewModel.Gender,
                    PictureFormat = patientViewModel.Picture.ContentType
                };
                var memoryStream = new MemoryStream();
                patientViewModel.Picture.CopyTo(memoryStream);
                patient.Picture = memoryStream.ToArray();

                repository.AddPatient(patient);
                return RedirectToAction("DetailView", new { id = patient.PatientId });
            }
            else return View();
        }

        [Authorize(Policy = "InternOrPhysioOnly")]
        [HttpGet]
        public ActionResult EditPatientView(int id)
        {
            AddPhysioToList();
            AddPatientToList();
            Patient patient = repository.GetPatient(id);

            PatientViewModel patientViewModel = new PatientViewModel
            {
                Name = patient.Name,
                Email = patient.Email,
                Phone = patient.Phone,
                Occupation = patient.Occupation,
                OccupationId = patient.OccupationId,
                Birthday = patient.Birthday,
                Gender = patient.Gender,
                PatientId = patient.PatientId
            };

            ViewBag.Pic = Convert.ToBase64String(patient.Picture);
            ViewBag.PicFormat = patient.PictureFormat;


            return View(patientViewModel);
        }

        [Authorize(Policy = "InternOrPhysioOnly")]
        [HttpPost]
        public ActionResult EditPatientView(PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                Patient oldPatient = repository.GetPatient(patientViewModel.PatientId);
                var patient = new Patient
                {
                    PatientId = patientViewModel.PatientId,
                    Name = patientViewModel.Name,
                    Email = patientViewModel.Email,
                    Phone = patientViewModel.Phone,
                    Occupation = patientViewModel.Occupation,
                    OccupationId = patientViewModel.OccupationId,
                    Birthday = patientViewModel.Birthday,
                    Gender = patientViewModel.Gender
                };
                if (patientViewModel.EditedPicture != null)
                {
                    var memoryStream = new MemoryStream();
                    patientViewModel.EditedPicture.CopyTo(memoryStream);
                    patient.Picture = memoryStream.ToArray();
                    patient.PictureFormat = patientViewModel.EditedPicture.ContentType;
                }
                else
                {
                    patient.Picture = oldPatient.Picture;
                    patient.PictureFormat = oldPatient.PictureFormat;
                }


                repository.UpdatePatient(patient);
                return RedirectToAction("DetailView", new { id = patient.PatientId });
            }
            else return View(patientViewModel);
        }

        [Authorize(Policy = "InternOrPhysioOnly")]
        public IActionResult PatientList()
        {
            AddPhysioToList();
            AddPatientToList();
            return View("PatientList", repository.Patients());
        }

        [Authorize(Policy = "InternOrPhysioOnly")]
        [HttpGet]
        public ActionResult DetailView(int id)
        {
            AddPhysioToList();
            AddPatientToList();
            Patient patient = repository.GetPatient(id);
            PatientFile patientFile = fileRepository.FindFileWithPatientId(id);
            AddSessionsInViewbag(patient.Email);
            AddAppointmentsInViewbag(patient.Email);

            if (patientFile != null) AddRemarksOfFileToViewBag(patientFile.Id);

            var viewModel = new PatientDisplayViewModel
            {
                PatientId = patient.PatientId,
                Name = patient.Name,
                Email = patient.Email,
                Phone = patient.Phone,
                Occupation = patient.Occupation,
                OccupationId = patient.OccupationId,
                Birthday = patient.Birthday,
                Gender = patient.Gender
            };
            viewModel.Picture = Convert.ToBase64String(patient.Picture);
            viewModel.PictureFormat = patient.PictureFormat;

            physiotherapistRepo.Physiotherapists();
            treatmentIRepo.Treatments();

            ViewBag.PatientFile = patientFile;
            return View("DetailView", viewModel);
        }
    }
}
