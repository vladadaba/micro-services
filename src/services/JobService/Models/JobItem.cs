using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using JobService.Commands;
using DatabaseUtils;

namespace JobService.Models
{
    [Table(Table)]
    public class JobItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public JobStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SerialNumber { get; internal set; }

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

        internal const string Table = "job_items"; // nameof(JobContext.JobItems).ToSnakeCase();

        internal static class Columns
        {
            public static string Id { get; } = nameof(JobItem.Id).ToSnakeCase();
            public static string Name { get; } = nameof(JobItem.Name).ToSnakeCase();
            public static string Status { get; } = nameof(JobItem.Status).ToSnakeCase();
            public static string CreatedAt { get; } = nameof(JobItem.CreatedAt).ToSnakeCase();
            public static string SerialNumber { get; } = nameof(JobItem.SerialNumber).ToSnakeCase();
        }
    }
}
