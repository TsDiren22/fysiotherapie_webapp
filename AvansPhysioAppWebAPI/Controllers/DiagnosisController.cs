using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AvansPhysioAppWebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class DiagnosisController : ControllerBase
    {

        private readonly ILogger<DiagnosisController> _logger;
        private IDiagnosisRepo _diagnosisRepo;

        public DiagnosisController(ILogger<DiagnosisController> logger, IDiagnosisRepo diagnosisRepo)
        {
            _logger = logger;
            this._diagnosisRepo = diagnosisRepo;
        }

        [HttpGet("{id}")]
        public ActionResult<Diagnosis> Get(string id)
        {
            return Ok(_diagnosisRepo.GetDiagnosis(id));
        }

        [HttpGet]
        public ActionResult<List<Diagnosis>> Get([FromQuery] string locationOnBody, [FromQuery] string pathology)
        {
            if (locationOnBody != null && pathology != null) return Ok(_diagnosisRepo.GetDiagnosisByParameters(locationOnBody, pathology));
            else if (locationOnBody != null) return Ok(_diagnosisRepo.GetDiagnosesByLocationOnBody(locationOnBody));
            else if (pathology != null) return Ok(_diagnosisRepo.GetDiagnosesByPathology(pathology));
            else return Ok(_diagnosisRepo.Diagnosis().ToList());
        }
    }
}
