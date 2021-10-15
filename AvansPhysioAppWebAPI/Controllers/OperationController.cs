using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomainServices.DomainServices;
using Microsoft.AspNetCore.Mvc.Diagnostics;

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
        public ActionResult<List<Operation>> Get()
        {
            return Ok(operationIRepo.Operations().ToList());
        }

    }
}
