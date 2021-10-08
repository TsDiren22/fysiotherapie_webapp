using System.Collections.Generic;
using System.Linq;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AvansFysioApp.Controllers
{
    //[AllowAnonymous]
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

        //[Authorize(Policy = "PhysiotherapistOnly")]
        [HttpGet]
        public ViewResult AddPatientView()
        {
            return View();
        }

        //[Authorize(Policy = "PhysiotherapistOnly")]
        [HttpPost]
        public ViewResult AddPatientView(Patient patient)
        {
            if (ModelState.IsValid)
            {
                repository.AddPatient(patient);
                return View("PatientList", repository.Patients());
            }   
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ViewResult EditPatientView(int id)
        {
            //Krijg patient id mee en stuur het naar de edit view van patient
            Patient patient = repository.GetPatient(id);
            return View(patient);
        }

        [HttpPost]
        public ViewResult EditPatientView(Patient patient)
        {
            if (ModelState.IsValid)
            {
                repository.UpdatePatient(patient);
                return View("PatientList", repository.Patients());
            }
            else
            {
                return View();
            }
        }
        
        public IActionResult PatientList()
        {
            return View("PatientList", repository.Patients());
        }

        [HttpGet]
        public ViewResult DetailView(int id)
        {
            Patient patient = repository.GetPatient(id);
            return View("DetailView", patient);
        }

        [HttpGet]
        public ViewResult AddFileView()
        {
            var patients = repository.Patients().Prepend(new Patient(){ PatientId = -1, Name = "Select a patient"});
            ViewBag.Patients = new SelectList(patients, "PatientId", "Name");


            var physios = physiotherapistRepo.physiotherapists().Prepend(new Physiotherapist() {Id = -1, Name = "Select a physiotherapist"});
            ViewBag.Physiotherapists = new SelectList(physios, "Id", "Name");


            return View();
        }



        [HttpPost]
        public ViewResult AddFileView(PatientFile patientFile)
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
                return View("DetailView", patient);
            }
            else
            {
                return View();
            }
        }

    }
}