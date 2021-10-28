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

        [HttpGet("{id}")]
        public ActionResult<Diagnosis> Get(string id)
        {
            return Ok(diagnosisIRepo.GetDiagnosis(id));
        }

        [HttpGet]
        public ActionResult<List<Diagnosis>> Get([FromQuery] string locationOnBody, [FromQuery] string pathology)
        {
            if (locationOnBody != null && pathology != null) return Ok(diagnosisIRepo.GetDiagnosisByParameters(locationOnBody, pathology));
            else if (locationOnBody != null) return Ok(diagnosisIRepo.GetDiagnosesByLocationOnBody(locationOnBody));
            else if (pathology != null) return Ok(diagnosisIRepo.GetDiagnosesByPathology(pathology));
            else return Ok(diagnosisIRepo.Diagnosis().ToList());
        }
    }
}
