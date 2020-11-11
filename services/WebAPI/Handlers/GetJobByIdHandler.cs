using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.DTO;
using WebAPI.Models;
using WebAPI.Queries;

namespace WebAPI.Handlers
{
    public class GetJobByIdHandler : IRequestHandler<GetJobByIdQuery, JobResponse?>
    {
        private readonly JobContext _jobContext;

        public GetJobByIdHandler(JobContext jobContext)
        {
            _jobContext = jobContext;
        }

        public async Task<JobResponse?> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _jobContext.JobItems.FindAsync(request.Id);
            if (job == null)
            {
                return null;
            }

            return JobResponse.From(job);
        }
    }
}
