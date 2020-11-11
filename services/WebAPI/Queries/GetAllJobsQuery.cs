using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using WebAPI.DTO;

namespace WebAPI.Queries
{
    public class GetAllJobsQuery : IRequest<IEnumerable<JobResponse>>
    {
    }
}
