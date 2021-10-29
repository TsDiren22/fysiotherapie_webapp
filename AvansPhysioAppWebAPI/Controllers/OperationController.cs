using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;

namespace AvansPhysioAppWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationController : ControllerBase
    {

        private readonly ILogger<OperationController> _logger;
        private OperationIRepo operationIRepo;

        public OperationController(ILogger<OperationController> logger, OperationIRepo operationIRepo)
        {
            _logger = logger;
            this.operationIRepo = operationIRepo;
        }

        [HttpGet]
        public ActionResult<List<Operation>> Get([FromQuery] bool mandatory, [FromQuery] string description)
        {
            if (description != null) return Ok(operationIRepo.GetOperationByDescription(description));
            else return Ok(operationIRepo.Operations().ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Operation> Get(string id)
        {
            return Ok(operationIRepo.GetOperation(id));
        }

    }
}
