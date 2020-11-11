using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTO;
using WebAPI.Models;
using WebAPI.Queries;

namespace WebAPI.Handlers
{
    public class GetAllJobsHandler : IRequestHandler<GetAllJobsQuery, IEnumerable<JobResponse>>
    {
        private readonly JobContext _jobContext;

        public GetAllJobsHandler(JobContext jobContext)
        {
            _jobContext = jobContext;
        }

        public async Task<IEnumerable<JobResponse>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobContext.JobItems.ToListAsync();

            return jobs.Select(job => JobResponse.From(job));
        }
    }
}
