using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Models;

namespace WebAPI.Commands
{
    public class CreateJobCommand : IRequest
    {
        public CreateJobCommand(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Status = JobStatus.Pending;
        }

        public Guid Id { get; }
        public string Name { get; }
        public JobStatus Status { get; }
    }
}
