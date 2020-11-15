using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using MediatR;
using JobService.DTO;
using JobService.Models;
using JobService.Queries;
using DatabaseUtils;

namespace JobService.Handlers
{
    public class GetJobByIdHandler : IRequestHandler<GetJobByIdQuery, JobResponse?>
    {
        private readonly IConnectionFactory _factory;

        public GetJobByIdHandler(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<JobResponse?> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            using var conn = _factory.GetConnection();
            var job = await conn.GetAsync<JobItem>(request.Id);
            
            return job != null ? JobResponse.From(job) : null;
        }
    }
}
