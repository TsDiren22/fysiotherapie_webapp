using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using AvansFysioApp.ExtentionMethods;
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
        private HttpClient client;
        private OperationIRepo operationIRepo;
        private IDiagnosisRepo diagnosisRepo;

        public HomeController(IRepo repository, IDiagnosisRepo diagnosisRepo, PatientFileIRepo fileRepository, IPhysiotherapistRepo physiotherapistRepo, OperationIRepo operationIRepo, IConfiguration configuration)
        {
            this.repository = repository;
            this.fileRepository = fileRepository;
            this.physiotherapistRepo = physiotherapistRepo;
            this.client = new HttpClient()
            {
                BaseAddress = new Uri(configuration.GetConnectionString("BaseUrl"))
            };
            this.operationIRepo = operationIRepo;
            this.diagnosisRepo = diagnosisRepo;
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