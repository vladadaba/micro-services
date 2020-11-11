using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Commands;

namespace WebAPI.Models
{
    public class JobItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public JobStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public static JobItem From(CreateJobCommand command)
        {
            return new JobItem
            {
                Id = command.Id,
                Name = command.Name,
                Status = command.Status,
                CreatedAt = command.CreatedAt
            };
        }
    }
}
