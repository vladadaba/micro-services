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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* // this won't work because timestamps are not unique
            // indexes cannot be added using data annotations
            modelBuilder.Entity<JobItem>()
                .HasIndex(j => j.Timestamp)
                .HasMethod("btree");
            */
            /* // this won't work because we cant add serial column to existing table (cant alter table to add serial column)
            modelBuilder.Entity<JobItem>()
                .Property(j => j.SerialNumber)
                .UseSerialColumn(); // Selects the serial column strategy, which is a regular column backed by an auto-created index.
            */
            modelBuilder.HasSequence<int>("seq_jobs_number")
                .StartsAt(1);
            modelBuilder.Entity<JobItem>()
                .Property(j => j.SerialNumber)
                .HasDefaultValueSql("nextval('seq_jobs_number')");
        }

        public DbSet<JobItem> JobItems => Set<JobItem>();
        public DbSet<OutboxItem> OutboxItems => Set<OutboxItem>();

    }
}
