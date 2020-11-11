using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class OutboxItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public JobStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public static OutboxItem From(JobItem job)
        {
            return new OutboxItem
            {
                Id = job.Id,
                Name = job.Name,
                Status = job.Status,
                CreatedAt = job.CreatedAt
            };
        }
    }
}
