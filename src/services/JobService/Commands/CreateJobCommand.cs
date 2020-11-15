using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using JobService.DTO;
using JobService.Models;

namespace JobService.Commands
{
    public class CreateJobCommand : IRequest
    {
        public CreateJobCommand(JobRequest req)
        {
            Id = Guid.NewGuid();
            Name = req.Name;
            Status = JobStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; }
        public string Name { get; }
        public JobStatus Status { get; }
        public DateTime CreatedAt { get; set; }

    }
}
