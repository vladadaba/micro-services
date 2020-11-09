using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class JobItem
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

        public static JobItem NewJob(string name)
        {
            return new JobItem
            {
                Id = Guid.NewGuid(),
                Name = name,
                Status = JobStatus.Pending
            };
        }
    }
}
