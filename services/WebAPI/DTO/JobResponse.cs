using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO
{
    public class JobResponse
    {
        public JobResponse()
        {

        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public JobStatus Status { get; set; }
    }
}
