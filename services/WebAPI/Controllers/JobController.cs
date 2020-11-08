using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.DTO;
using WebAPI.Messaging;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class JobController : ControllerBase
    {
        private readonly MessageProducer _producer;
        private readonly ILogger<JobController> _logger;

        public JobController(MessageProducer producer, ILogger<JobController> logger)
        {
            _producer = producer;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("{id}")]
        public JobResponse Get(Guid id)
        {
            return new JobResponse();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] JobRequest req)
        {
            _producer.SendMessage(req.Name);

            return CreatedAtAction(nameof(Get), new { id = Guid.NewGuid() }, new { test = "test" });
        }
    }
}
