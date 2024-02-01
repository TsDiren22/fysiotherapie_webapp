using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using Microsoft.AspNetCore.Authorization;

namespace AvansFysioApp.Controllers
{
    public class PatientController : Controller
    {
        private IRepo repository;
        private PatientFileIRepo fileRepository;
        private IPhysiotherapistRepo physiotherapistRepo;
        private TreatmentPlanIRepo treatmentPlanIRepo;
        private TreatmentIRepo treatmentIRepo;
        private SessionIRepo sessionIRepo;
        private RemarkIRepo remarkIRepo;

        public PatientController(IRepo repository, RemarkIRepo remarkIRepo, PatientFileIRepo fileRepository, IPhysiotherapistRepo physiotherapistRepo, TreatmentPlanIRepo treatmentPlanIRepo, TreatmentIRepo treatmentIRepo, SessionIRepo sessionIRepo)
        {
            this.repository = repository;
            this.fileRepository = fileRepository;
            this.physiotherapistRepo = physiotherapistRepo;
            this.remarkIRepo = remarkIRepo;
            this.sessionIRepo = sessionIRepo;
            this.treatmentIRepo = treatmentIRepo;
            this.treatmentPlanIRepo = treatmentPlanIRepo;
        }

        public void AddPhysioToList()
        {
            ViewBag.Physio = physiotherapistRepo.Physiotherapists();
        }

        public void AddPatientToList()
        {
            ViewBag.Pat = repository.Patients();
        }

        public void AddTreatmentsToListByPatientList(int id)
        {
            List<Treatment> list = new List<Treatment>();
            foreach (Treatment treatment in treatmentIRepo.Treatments().Where((x) => x.PatientFileId == id))
            {
                list.Add(treatment);
            }
            ViewBag.Treatments = list;
        }

        public void AddAppointmentsInViewbag(string email)
        {
            Patient patient = repository.GetPatientByEmail(email);
            Physiotherapist physiotherapist = physiotherapistRepo.getPhysiotherapistByEmail(email);
            List<Session> list = new List<Session>();

            if (patient != null)
            {
                foreach (var a in sessionIRepo.Sessions())
                {
                    if (a!= null && a.PatientId == patient.PatientId)
                    {
                        list.Add(a);
                    }
                }
            }
            else if (physiotherapist != null)
            {
                foreach (var a in sessionIRepo.Sessions())
                {
                    if (a != null && a.HeadPhysiotherapistId == physiotherapist.Id)
                    {
                        list.Add(a);
                    }
                }
            }
            ViewBag.AppointmentList = list;
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
                if (repository.GetPatientByEmail(patientViewModel.Email) != null | physiotherapistRepo.getPhysiotherapistByEmail(patientViewModel.Email) != null)
                {
                    ModelState.AddModelError("", "Email is already in use.");
                    return View(patientViewModel);
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
        
        [HttpGet]
        public ActionResult DetailView(int id)
        {
            AddPhysioToList();
            AddPatientToList();
            Patient patient = repository.GetPatient(id);
            PatientFile patientFile = fileRepository.FindFileWithPatientId(id);
            AddAppointmentsInViewbag(patient.Email);

            if (patientFile != null)
            {
                AddRemarksOfFileToViewBag(patientFile.Id);
                ViewBag.Tp = treatmentPlanIRepo.GetTreatmentPlan(patientFile.Id);
                AddTreatmentsToListByPatientList(patientFile.Id);
            }

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
