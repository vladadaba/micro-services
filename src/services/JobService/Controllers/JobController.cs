using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JobService.Commands;
using JobService.DTO;
using JobService.Queries;

namespace JobService.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<JobController> _logger;

        public JobController(IMediator mediator, ILogger<JobController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JobResponse>> Get(Guid id)
        {
            _logger.LogInformation($"Get job with id = '{id}'");
            var query = new GetJobByIdQuery(id);
            var response = await _mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            return response;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<JobResponse>> GetAll([FromQuery] JobFilter filter)
        {
            _logger.LogInformation($"Get all jobs");
            var query = new GetAllJobsQuery(filter);
            
            return await _mediator.Send(query);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] JobRequest req)
        {
            _logger.LogInformation($"Post job with name = '{req.Name}'");
            var command = new CreateJobCommand(req);
            await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = command.Id }, null);
        }
    }
}
