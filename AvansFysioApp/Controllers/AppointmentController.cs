using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppDomainServices.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AvansFysioApp.Controllers
{
    public class AppointmentController : Controller
    {

        private IRepo repository;
        private PatientFileIRepo fileRepository;
        private IPhysiotherapistRepo physiotherapistRepo;
        private TreatmentPlanIRepo treatmentPlanIRepo;
        private SessionIRepo sessionIRepo;
        private UserManager<IdentityUser> userManager;
        private AppointmentValidation appointmentValidation = new AppointmentValidation();

        public AppointmentController(IRepo repository, PatientFileIRepo fileRepository, IPhysiotherapistRepo physiotherapistRepo, TreatmentPlanIRepo treatmentPlanIRepo, SessionIRepo sessionIRepo, UserManager<IdentityUser> userManager)
        {
            this.repository = repository;
            this.fileRepository = fileRepository;
            this.physiotherapistRepo = physiotherapistRepo;
            this.sessionIRepo = sessionIRepo;
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

        public void AddInternInViewbag()
        {

            List<Physiotherapist> isIntern = new List<Physiotherapist>();

            foreach (Physiotherapist p in physiotherapistRepo.Physiotherapists())
            {
                if (p.IsIntern)
                {
                    isIntern.Add(p);
                }
            }

            var physios = isIntern.Prepend(new Physiotherapist() { Id = -1, Name = "(Optional) Leave empty to choose head practitioner." });
            ViewBag.Intern = new SelectList(physios, "Id", "Name");
        }

        public bool IsIntern(string email)
        {

            Physiotherapist isIntern = new Physiotherapist();

            foreach (Physiotherapist p in physiotherapistRepo.Physiotherapists())
            {
                if (p.Email.Equals(email) && p.IsIntern)
                {
                    return true;
                }
            }

            return false;
        }


        public void AddTreatmentPlanToViewbagByEmail(string email)
        {
            var patient = repository.GetPatientByEmail(email);
            List<TreatmentPlan> list = new List<TreatmentPlan>();

            if (patient != null)
            {
                PatientFile patientFile = fileRepository.FindFileWithPatientId(patient.PatientId);
                TreatmentPlan treatmentPlan = treatmentPlanIRepo.FindTreatmentPlanWithPatientFile(patientFile.Id);
                list.Add(treatmentPlan);
            }
            else
            {
                var physio = physiotherapistRepo.getPhysiotherapistByEmail(email);
                var treatmentPlans = treatmentPlanIRepo.TreatmentPlans();
                foreach (TreatmentPlan treatmentPlan in treatmentPlans)
                {
                    list.Add(treatmentPlan);
                }
            }
            ViewBag.TreatmentPlans = new SelectList(list, "Id", "Title");
        }

        [HttpGet]
        public async Task<IActionResult> AddSession()
        {
            var user = await userManager.GetUserAsync(User);
            AddTreatmentPlanToViewbagByEmail(user.Email);
            AddInternInViewbag();
            ViewBag.IsIntern = IsIntern(user.Email);
            ViewBag.IsPatient = repository.GetPatientByEmail(user.Email);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSession(Session session)
        {
            AddPhysioToList();
            AddPatientToList();
            AddPatientsInViewbag();
            AddInternInViewbag();
            var user = await userManager.GetUserAsync(User);
            ViewBag.IsPatient = repository.GetPatientByEmail(user.Email);
            ViewBag.IsIntern = IsIntern(user.Email);
            AddTreatmentPlanToViewbagByEmail(user.Email);

            if (ModelState.IsValid)
            {
                TreatmentPlan treatmentPlan = treatmentPlanIRepo.GetTreatmentPlan(session.TreatmentPlanId);
                int size = 0;
                PatientFile patientFile = fileRepository.GetPatientFile(treatmentPlan.FileId);
                Physiotherapist physiotherapist = physiotherapistRepo.GetPhysiotherapist((int)patientFile.HeadPractitionerId);
                Patient p = repository.GetPatient((int)patientFile.PatientId);

                if (session.HeadPhysiotherapistId == -1 || session.HeadPhysiotherapistId == null)
                {
                    session.HeadPhysiotherapistId = physiotherapist.Id;
                }
                else
                {
                    physiotherapist = physiotherapistRepo.GetPhysiotherapist((int)session.HeadPhysiotherapistId);
                }



                session.AppointmentMade = DateTime.Now;
                session.PatientId = p.PatientId;
                session.AppointmentEnd = session.AppointmentBegin.AddMinutes(treatmentPlan.Duration);

                List<Session> sessions = sessionIRepo.GetSessionsWithTreatmentPlanId(treatmentPlan.Id).ToList();

                bool physiotherapistAvailable = appointmentValidation.PhysioIsAvailable(
                    sessionIRepo.GetSessionsWithPhysiotherapistId(physiotherapist.Id).ToList(), session,
                    physiotherapist);


                if (appointmentValidation.CheckIfSessionsPerWeekWillBeExceeded(session, treatmentPlan.MaxSessions, sessions) || !physiotherapistAvailable || patientFile.DateOfEnd < session.AppointmentEnd)
                {
                    if (appointmentValidation.CheckIfSessionsPerWeekWillBeExceeded(session, treatmentPlan.MaxSessions, sessions))
                    {
                        ModelState.AddModelError("", "The maximum amount of sessions this week have been reached.");
                    }

                    if (!physiotherapistAvailable)
                    {
                        ModelState.AddModelError("", "The physiotherapist is unavailable at this time of the day. This physiotherapist is available between " + physiotherapist.AvailabilityStart.ToString("HH:mm") + " and " + physiotherapist.AvailabilityEnd.AddMinutes(-treatmentPlan.Duration).ToString("HH:mm") + "!");
                    }

                    if (patientFile.DateOfEnd < session.AppointmentEnd)
                    {
                        ModelState.AddModelError("", "Appointment must be set before the date of firing: " + patientFile.DateOfEnd.Value.ToString("dd-MM-yyyy"));
                    }
                    return View(session);
                }

                sessionIRepo.AddSession(session);

                return RedirectToAction("DetailView", "Patient", new { id = p.PatientId });
            }
            return View(session);
        }
        
        public IActionResult DeleteSession(int id, string returnUrl = "Appointment/AppointmentList")
        {
            Session session = sessionIRepo.GetSession(id);
            if (appointmentValidation.CheckHoursBeforeAppointmentDeleteIsNotTwentyFour(session))
            {
                return Redirect("../../" + returnUrl);
            }

            sessionIRepo.DeleteSessionWithSessionId(id);
            return Redirect("../../" + returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> EditSession(int id)
        {
            var s = sessionIRepo.GetSession(id);
            AddInternInViewbag();
            var user = await userManager.GetUserAsync(User);
            ViewBag.IsPatient = repository.GetPatientByEmail(user.Email);
            ViewBag.IsIntern = IsIntern(user.Email);
            return View(s);
        }

        [HttpPost]
        public async Task<IActionResult> EditSession(Session session)
        {
            AddInternInViewbag();
            var user = await userManager.GetUserAsync(User);
            ViewBag.IsPatient = repository.GetPatientByEmail(user.Email);
            ViewBag.IsIntern = IsIntern(user.Email);
            if (ModelState.IsValid)
            {
                var s = sessionIRepo.GetSession(session.Id);
                var treatmentPlan = treatmentPlanIRepo.GetTreatmentPlan(s.TreatmentPlanId);
                int size = 0;

                if (session.HeadPhysiotherapistId == -1 || session.HeadPhysiotherapistId == null)
                {
                    session.HeadPhysiotherapistId = s.HeadPhysiotherapistId;
                }
                else
                {
                    s.HeadPhysiotherapistId = session.HeadPhysiotherapistId;
                }

                Physiotherapist physiotherapist = physiotherapistRepo.GetPhysiotherapist((int)session.HeadPhysiotherapistId);

                int duration = treatmentPlan.Duration;
                s.AppointmentBegin = session.AppointmentBegin;
                session.AppointmentEnd = s.AppointmentBegin.AddMinutes(duration);
                s.AppointmentEnd = s.AppointmentBegin.AddMinutes(duration);

                PatientFile patientFile = fileRepository.GetPatientFile(treatmentPlan.FileId);
                bool physiotherapistAvailable = appointmentValidation.timeOfPhysio(s.AppointmentBegin, s.AppointmentEnd,
                    physiotherapist.AvailabilityStart, physiotherapist.AvailabilityEnd);

                List<Session> sessions = sessionIRepo.GetSessionsWithTreatmentPlanId(treatmentPlan.Id).Where(x => x.Id != s.Id).ToList();

                if (appointmentValidation.CheckIfSessionsPerWeekWillBeExceeded(s, treatmentPlan.MaxSessions, sessions) || physiotherapistAvailable || patientFile.DateOfEnd < session.AppointmentEnd)
                {
                    if (appointmentValidation.CheckIfSessionsPerWeekWillBeExceeded(s, treatmentPlan.MaxSessions, sessions))
                    {
                        ModelState.AddModelError("", "The maximum amount of sessions this week have been reached.");
                    }

                    if (physiotherapistAvailable)
                    {
                        ModelState.AddModelError("", "The physiotherapist is unavailable at this time of the day. Please try between " + physiotherapist.AvailabilityStart.ToString("HH:mm") + " and " + physiotherapist.AvailabilityEnd.AddMinutes(-treatmentPlan.Duration).ToString("HH:mm") + "!");
                    }
                    if (patientFile.DateOfEnd < session.AppointmentEnd)
                    {
                        ModelState.AddModelError("", "Appointment must be set before the date of firing: " + patientFile.DateOfEnd.Value.ToString("dd-MM-yyyy"));
                    }
                    return View(session);
                }

                sessionIRepo.UpdateSession(s);
                return RedirectToAction("DetailView", "Patient", new { id =  s.PatientId});
            }
            return View(session);
        }

        [HttpGet]
        public async Task<IActionResult> AppointmentList(DateTime? date = null)
        {
            date ??= DateTime.Now;

            var user = await userManager.GetUserAsync(User);
            var patient = repository.GetPatientByEmail(user.Email);
            var physio = physiotherapistRepo.getPhysiotherapistByEmail(user.Email);

            if (patient != null)
            {
                PatientFile patientFile = fileRepository.FindFileWithPatientId(patient.PatientId);
                var list = sessionIRepo.GetSessionsWithPatientId(patient.PatientId).ToList();
                list.Sort((x, y) => x.AppointmentBegin.CompareTo(y.AppointmentBegin));
                ViewBag.IsPatient = true;
                return View(list);
            }
            else
            {
                var list = sessionIRepo.GetSessionsWithPhysiotherapistId(physio.Id).Where((x) => x.AppointmentBegin.ToShortDateString().Equals(date.Value.ToShortDateString())).ToList();
                list.Sort((x, y) => x.AppointmentBegin.CompareTo(y.AppointmentBegin));
                ViewBag.IsPatient = false;
                return View(list);
            }
        }

    }
}
