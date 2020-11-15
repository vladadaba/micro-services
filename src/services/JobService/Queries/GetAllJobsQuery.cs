using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using JobService.DTO;

namespace JobService.Queries
{
    public class GetAllJobsQuery : IRequest<IEnumerable<JobResponse>>
    {
        public GetAllJobsQuery(JobFilter filter)
        {
            Filter = filter;
        }

        public JobFilter Filter { get; }
    }
}
