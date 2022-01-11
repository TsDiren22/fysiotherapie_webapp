using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private TreatmentPlanIRepo treatmentPlanIRepo;
        private TreatmentIRepo treatmentIRepo;
        private SessionIRepo sessionIRepo;
        private HttpClient client;
        private IConfiguration configuration;
        private UserManager<IdentityUser> userManager;
        private OperationIRepo operationIRepo;
        private IDiagnosisRepo diagnosisRepo;
        private RemarkIRepo remarkIRepo;

        public AppointmentController(IRepo repository, IDiagnosisRepo diagnosisRepo, RemarkIRepo remarkIRepo, PatientFileIRepo fileRepository, IPhysiotherapistRepo physiotherapistRepo, TreatmentPlanIRepo treatmentPlanIRepo, TreatmentIRepo treatmentIRepo, OperationIRepo operationIRepo, SessionIRepo sessionIRepo, IConfiguration configuration, UserManager<IdentityUser> userManager)
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

        public void AddPhysioToList()
        {
            ViewBag.Physio = physiotherapistRepo.Physiotherapists();
        }
        public void AddPatientToList()
        {
            ViewBag.Pat = repository.Patients();
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
        public async Task<IActionResult> AddSession()
        {
            var user = await userManager.GetUserAsync(User);
            List<TreatmentPlan> list = new List<TreatmentPlan>();
            var patient = repository.GetPatientByEmail(user.Email);

            if (patient != null)
            {
                PatientFile patientFile = fileRepository.FindFileWithPatientId(patient.PatientId);
                TreatmentPlan treatmentPlan = treatmentPlanIRepo.FindTreatmentPlanWithPatientFile(patientFile.Id);
                list.Add(treatmentPlan);
            }
            else
            {
                var physio = physiotherapistRepo.getPhysiotherapistByEmail(user.Email);
                var treatmentPlans = treatmentPlanIRepo.findTreatmentPlanWithPhysiotherapistId(physio.Id);
                foreach (TreatmentPlan treatmentPlan in treatmentPlans)
                {
                    list.Add(treatmentPlan);
                }
            }
            ViewBag.TreatmentPlans = new SelectList(list, "Id", "Title");
            return View();
        }

        [Authorize(Policy = "InternOrPhysioOnly")]
        [HttpPost]
        public async Task<IActionResult> AddSession(Session session)
        {
            AddPhysioToList();
            AddPatientToList();
            AddPatientsInViewbag();
            var user = await userManager.GetUserAsync(User);
            List<TreatmentPlan> list = new List<TreatmentPlan>();
            var patient = repository.GetPatientByEmail(user.Email);

            if (patient != null)
            {
                PatientFile patientFile = fileRepository.FindFileWithPatientId(patient.PatientId);
                TreatmentPlan treatmentPlan = treatmentPlanIRepo.FindTreatmentPlanWithPatientFile(patientFile.Id);
                list.Add(treatmentPlan);
            }
            else
            {
                var physio = physiotherapistRepo.getPhysiotherapistByEmail(user.Email);
                var treatmentPlans = treatmentPlanIRepo.findTreatmentPlanWithPhysiotherapistId(physio.Id);
                foreach (TreatmentPlan treatmentPlan in treatmentPlans)
                {
                    list.Add(treatmentPlan);
                }
            }
            ViewBag.TreatmentPlans = new SelectList(list, "Id", "Title");

            if (ModelState.IsValid)
            {
                TreatmentPlan treatmentPlan = treatmentPlanIRepo.GetTreatmentPlan(session.TreatmentPlanId);
                int size = 0;

                var currentCulture = CultureInfo.CurrentCulture;
                var weekNumber1 = currentCulture.Calendar.GetWeekOfYear(
                    session.AppointmentBegin,
                    currentCulture.DateTimeFormat.CalendarWeekRule,
                    currentCulture.DateTimeFormat.FirstDayOfWeek);
                foreach (Session s in sessionIRepo.GetSessionsWithTreatmentPlanId(treatmentPlan.Id))
                {
                    var weekNumber2 = currentCulture.Calendar.GetWeekOfYear(
                        s.AppointmentBegin,
                        currentCulture.DateTimeFormat.CalendarWeekRule,
                        currentCulture.DateTimeFormat.FirstDayOfWeek);
                    if (weekNumber1 == weekNumber2)
                    {
                        size++;
                    }
                }
                if (size >= treatmentPlan.MaxSessions)
                {
                    ModelState.AddModelError("", "The maximum amount of sessions this week have been reached.");
                    return View(session);
                }

                PatientFile patientFile = fileRepository.GetPatientFile(treatmentPlan.FileId);
                Physiotherapist physiotherapist = physiotherapistRepo.GetPhysiotherapist((int)patientFile.HeadPractitionerId);
                Patient p = repository.GetPatient((int)patientFile.PatientId);

                session.AppointmentMade = DateTime.Now;
                session.HeadPhysiotherapist = physiotherapist;
                session.PatientId = p.PatientId;
                session.AppointmentEnd = session.AppointmentBegin.AddMinutes(treatmentPlan.Duration);
                sessionIRepo.AddSession(session);

                return RedirectToAction("DetailView", "Patient", new { id = p.PatientId });
            }
            return View(session);
        }



    }
}
