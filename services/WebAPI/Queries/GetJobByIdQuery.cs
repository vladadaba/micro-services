using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using WebAPI.DTO;

namespace WebAPI.Queries
{
    public class GetJobByIdQuery : IRequest<JobResponse?>
    {
        public GetJobByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
