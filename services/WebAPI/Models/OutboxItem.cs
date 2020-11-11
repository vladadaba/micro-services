using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class OutboxItem
    {
        public enum JobStatus
        {
            Pending,
            Running,
            Completed,
            Failed
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public JobStatus Status { get; set; }

        public static OutboxItem FromJob(JobItem job)
        {
            return new OutboxItem
            {
                Id = job.Id,
                Name = job.Name,
                Status = (JobStatus)job.Status
            };
        }
    }
}
