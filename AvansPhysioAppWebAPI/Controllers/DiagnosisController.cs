using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;

namespace AvansPhysioAppWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiagnosisController : ControllerBase
    {

        private readonly ILogger<DiagnosisController> _logger;
        private DiagnosisIRepo diagnosisIRepo;

        public DiagnosisController(ILogger<DiagnosisController> logger, DiagnosisIRepo diagnosisIRepo)
        {
            _logger = logger;
            this.diagnosisIRepo = diagnosisIRepo;
        }

        [HttpGet]
        public ActionResult<List<Diagnosis>> Get()
        {
            return Ok(diagnosisIRepo.Diagnosis().ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Diagnosis> Get(string id)
        {
            return Ok(diagnosisIRepo.GetDiagnosis(id));
        }
    }
}
