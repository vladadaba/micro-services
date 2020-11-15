using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobService.Models
{
    public enum JobStatus
    {
        Pending,
        Running,
        Completed,
        Failed
    }
}
