using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.DTO;
using WebAPI.Messaging;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class JobController : ControllerBase
    {
        private readonly JobService _jobService;
        private readonly ILogger<JobController> _logger;

        public JobController(JobService jobService, ILogger<JobController> logger)
        {
            _jobService = jobService;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JobResponse>> Get(Guid id)
        {
            var job = await _jobService.Get(id);
            if (job == null)
            {
                return NotFound();
            }

            return JobResponse.From(job);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<JobResponse>> GetAll()
        {
            var jobs = await _jobService.GetAll();

            return jobs.Select(j => JobResponse.From(j));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] JobRequest req)
        {
            // TODO: FluentValidation
            if (string.IsNullOrWhiteSpace(req.Name))
            {
                return BadRequest();
            }

            var newJob = JobItem.NewJob(req.Name);

            await _jobService.CreateJob(newJob);

            return CreatedAtAction(nameof(Get), new { id = newJob.Id }, JobResponse.From(newJob));
        }
    }
}
