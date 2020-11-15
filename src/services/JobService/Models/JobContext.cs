using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JobService.Models
{
    public class JobContext : DbContext
    {
        public JobContext(DbContextOptions<JobContext> options) : base(options)
        {
        }

        public DbSet<JobItem> JobItems => Set<JobItem>();
        public DbSet<OutboxItem> OutboxItems => Set<OutboxItem>();

    }
}
