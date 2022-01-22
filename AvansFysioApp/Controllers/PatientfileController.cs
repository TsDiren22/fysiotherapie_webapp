using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioApp.ExtentionMethods;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppDomainServices.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AvansFysioApp.Controllers
{
    public class PatientfileController : Controller
    {

        private IRepo repository;
        private PatientFileIRepo fileRepository;
        private IPhysiotherapistRepo physiotherapistRepo;
        private TreatmentPlanIRepo treatmentPlanIRepo;
        private TreatmentIRepo treatmentIRepo;
        private UserManager<IdentityUser> userManager;
        private OperationIRepo operationIRepo;
        private RemarkIRepo remarkIRepo;
        private PatientValidation patientValidation;

        public PatientfileController(IRepo repository, RemarkIRepo remarkIRepo, PatientFileIRepo fileRepository, IPhysiotherapistRepo physiotherapistRepo, TreatmentPlanIRepo treatmentPlanIRepo, TreatmentIRepo treatmentIRepo, OperationIRepo operationIRepo, UserManager<IdentityUser> userManager)
        {
            this.repository = repository;
            this.fileRepository = fileRepository;
            this.physiotherapistRepo = physiotherapistRepo;
            this.remarkIRepo = remarkIRepo;
            this.operationIRepo = operationIRepo;
            this.treatmentIRepo = treatmentIRepo;
            this.treatmentPlanIRepo = treatmentPlanIRepo;
            this.userManager = userManager;
            patientValidation = new PatientValidation();
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

        public void AddPatientFilesWithoutTreatmentInViewbag()
        {
            List<PatientFile> list = new List<PatientFile>();
            foreach (PatientFile patientFile in fileRepository.Responses())
            {
                var tp = treatmentPlanIRepo.FindTreatmentPlanWithPatientFile(patientFile.Id);
                if (tp == null)
                {
                    list.Add(patientFile);
                }
            }
            var patientFiles = list.Prepend(new PatientFile() { Id = -1, Title = "Select a patient" });
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

        public void AddPhysiotherapistsExceptInternInViewbag()
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

        public void AddPatientsWithNoPatientFile()
        {
            List<Patient> list = new List<Patient>();
            foreach (var p in repository.Patients())
                if (fileRepository.FindFileWithPatientId(p.PatientId) == null) list.Add(p);
            ViewBag.PatientNoFile = new SelectList(list, "PatientId", "Name");
        }

        public List<Treatment> AddTreatmentsToListByPatientFile(int id)
        {
            List<Treatment> list = new List<Treatment>();
            foreach (Treatment treatment in treatmentIRepo.Treatments().Where((x) => x.PatientFileId == id))
            {
                list.Add(treatment);
            }
            return list;
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
            AddPhysioToList();
            AddPatientToList();
            AddPatientsWithNoPatientFile();
            AddPhysiotherapistsInViewbag();
            AddPhysiotherapistsExceptInternInViewbag();
            if (ModelState.IsValid)
            {
                if (patientFile.SupervisionById == -1 || patientFile.HeadPractitionerId == -1 || patientFile.DateOfRegister > DateTime.Now || patientFile.DateOfEnd < DateTime.Now)
                {
                    if (patientFile.SupervisionById == -1)
                    {
                        ModelState.AddModelError("", "Please select a supervisor.");
                    }

                    if (patientFile.HeadPractitionerId == -1)
                    {
                        ModelState.AddModelError("", "Head practitioner is required!");
                    }

                    if (patientFile.DateOfRegister > DateTime.Now)
                    {
                        ModelState.AddModelError("", "Date of register cannot be in the future!");
                    }
                    if (patientFile.DateOfEnd < DateTime.Now)
                    {
                        ModelState.AddModelError("", "Date of firing must be in the future!");
                    }
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
            AddPhysioToList();
            AddPatientToList();
            AddPatientsInViewbag();
            AddPhysiotherapistsInViewbag();
            AddPhysiotherapistsExceptInternInViewbag();
            if (ModelState.IsValid)
            {
                Physiotherapist physiotherapist = physiotherapistRepo.GetPhysiotherapist((int)patientFile.IntakeById) ?? null;

                if (physiotherapist == null || (physiotherapist.IsIntern && patientFile.SupervisionById == -1) || patientFile.HeadPractitionerId == -1 || patientFile.DateOfEnd < DateTime.Now)
                {
                    if (physiotherapist == null)
                    {
                        ModelState.AddModelError("", "Intake by is required!.");
                        return View(patientFile);
                    }

                    if (patientFile.HeadPractitionerId == -1)
                    {
                        ModelState.AddModelError("", "Head practitioner is required!");
                    }

                    if (physiotherapist.IsIntern && patientFile.SupervisionById == -1)
                    {
                        ModelState.AddModelError("", "Supervision is required while taking an intake as an intern.");
                    }
                    if (patientFile.DateOfEnd < DateTime.Now)
                    {
                        ModelState.AddModelError("", "Date of firing must be in the future!");
                    }
                    return View(patientFile);
                }

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
        public IActionResult TreatmentView(int id)
        {
            Treatment treatment = treatmentIRepo.GetTreatment(id);
            PatientFile patientFile = fileRepository.GetPatientFile(treatment.PatientFileId);
            ViewBag.Id = patientFile.PatientId;
            return View(AddTreatmentsToListByPatientFile(id));
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
                PatientFile patientFile = fileRepository.GetPatientFile(treatment.PatientFileId);

                bool invalid =
                    patientValidation.ReturnTrueWhenParticularitiesIsEmptyWithMandatoryExplanationTrue(operation,
                        treatment);

                bool invalidDate = patientValidation.ReturnTrueWhenDateIsOutsideRegistration(patientFile, treatment);


                if (invalid || invalidDate)
                {
                        if (invalid)
                        {
                            ModelState.AddModelError("", "Particularities are mandatory with this treatment!");
                        }
                        if (invalidDate)
                        {
                            ModelState.AddModelError("", "Date of treatment must be before the date of firing, and after the date of registration.");
                        }
                        TempData.Put("operation", operation.Value);
                        return View(treatment);
                }
                treatment.DateCreated = DateTime.Now;
                treatment.Description = operation.Description;
                treatment.Type = operation;

                if (treatment.PhysiotherapistId == -1)
                {
                    treatment.PhysiotherapistId = patientFile.HeadPractitionerId;
                }
                treatment.PatientFile = patientFile;

                try
                {
                    treatmentIRepo.AddTreatment(treatment);
                }
                catch
                {
                    ModelState.AddModelError("", "This treatment is already added. Please try another one!");
                    return View(treatment);
                }
                return RedirectToAction("DetailView", "Patient", new { id = treatment.PatientFileId });
            }
            return View(treatment);
        }

        [HttpGet]
        public IActionResult EditTreatment(int id)
        {
            Treatment treatment = treatmentIRepo.GetTreatment(id);
            AddPhysiotherapistsInViewbag();
            AddPatientToList();
            AddPatientFilesInViewbag();
            return View(treatment);
        }

        [HttpPost]
        public IActionResult EditTreatment(Treatment treatment)
        {
            AddPhysiotherapistsInViewbag();
            AddPatientToList();
            AddPatientFilesInViewbag();
            if (ModelState.IsValid)
            {
                Operation operation = operationIRepo.GetOperation(treatment.TypeId);
                PatientFile patientFile = fileRepository.GetPatientFile(treatment.PatientFileId);


                if ((operation.MandatoryExplanation && treatment.Particularities == null) || treatment.DateOfTreatment > patientFile.DateOfEnd || (treatment.DateOfTreatment < patientFile.DateOfRegister))
                {
                    if (operation.MandatoryExplanation && treatment.Particularities == null)
                    {
                        ModelState.AddModelError("", "Particularities are mandatory with this treatment!");
                    }
                    if (treatment.DateOfTreatment > patientFile.DateOfEnd)
                    {
                        ModelState.AddModelError("", "Date of treatment must be before the date of firing.");
                    }
                    if (treatment.DateOfTreatment < patientFile.DateOfRegister)
                    {
                        ModelState.AddModelError("", "Date of treatment must be after the starting date of the patient.");
                    }
                    return View(treatment);
                }

                if (treatment.PhysiotherapistId == -1)
                {
                    treatment.PhysiotherapistId = patientFile.HeadPractitionerId;
                }
                
                treatmentIRepo.UpdateTreatment(treatment);
                return RedirectToAction("TreatmentView", "Patientfile", new { id = treatment.PatientFileId });
            }
            return View(treatment);
        }

        [HttpGet]
        public IActionResult DeleteTreatment(int id)
        {
            Treatment treatment = treatmentIRepo.GetTreatment(id);
            treatmentIRepo.DeleteTreatmentWithTreatmentId(id);
            return RedirectToAction("TreatmentView", "Patientfile", new { id = treatment.PatientFileId });
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
                return RedirectToAction("DetailView", "Patient", new { id = file.PatientId });
            }

            return View();
        }

        public IActionResult TreatmentView()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddTreatmentPlan()
        {
            AddPatientFilesWithoutTreatmentInViewbag();
            return View();
        }

        [HttpPost]
        public IActionResult AddTreatmentPlan(TreatmentPlan treatment)
        {
            if (ModelState.IsValid)
            {
                PatientFile patientFile = fileRepository.GetPatientFile(treatment.FileId);
                Physiotherapist physiotherapist = physiotherapistRepo.GetPhysiotherapist((int)patientFile.HeadPractitionerId);
                treatment.PhysiotherapistId = physiotherapist.Id;
                treatmentPlanIRepo.AddTreatmentPlan(treatment);
                return RedirectToAction("DetailView", "Patient", new { id = patientFile.PatientId });
            }
            AddPatientFilesInViewbag();
            return View(treatment);
        }
    }
}
