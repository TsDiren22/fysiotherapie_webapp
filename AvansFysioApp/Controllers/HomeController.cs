using System.Collections.Generic;
using System.Linq;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AvansFysioApp.Controllers
{
    public class HomeController : Controller
    {
        private IRepo repository;
        private PatientFileIRepo fileRepository;
        private IPhysiotherapistRepo physiotherapistRepo;

        public HomeController(IRepo repository, PatientFileIRepo fileRepository, IPhysiotherapistRepo physiotherapistRepo)
        {
            this.repository = repository;
            this.fileRepository = fileRepository;
            this.physiotherapistRepo = physiotherapistRepo;
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

        [HttpGet]
        public ActionResult EditPatientView(int id)
        {
            Patient patient = repository.GetPatient(id);
            return View(patient);
        }

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

        [HttpGet]
        public ActionResult AddFileView()
        {
            AddPatientsInViewbag();
            AddPhysiotherapistsInViewbag();
            return View();
        }

        [HttpPost]
        public ActionResult AddFileView(PatientFile patientFile)
        {
            if (ModelState.IsValid)
            {
                Patient patient = null;

                foreach (Patient p in repository.Patients())
                {
                    if (p.PatientId == patientFile.PatientId)
                    {
                        patient = p;
                    }
                }

                fileRepository.AddPatientFile(patientFile);
                return RedirectToAction("DetailView", new { id = patientFile.PatientId });

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

    }
}