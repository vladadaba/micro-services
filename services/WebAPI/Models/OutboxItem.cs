using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebAPI.Utils;

namespace WebAPI.Models
{
    public class OutboxItem
    {
        public Guid Id { get; set; } // unique id in Outbox table accross all types

        [MaxLength(255)]
        public string AggregateType { get; set; } = ""; // will be used to form kafka topic outbox.event.<aggregatetype>

        [MaxLength(255)]
        public string AggregateId { get; set; } = ""; // actual id

        [MaxLength(255)]
        public string Type { get; set; } = "";

        public DateTime Timestamp { get; set; }

        public string CorrelationId { get; set; } = "";

        [Column(TypeName = "jsonb")]
        public string? Payload { get; set; } = "";

        public static OutboxItem From(JobItem job)
        {
            return new OutboxItem
            {
                Id = Guid.NewGuid(),
                AggregateId = job.Id.ToString(),
                AggregateType = "job",
                Type = "Job",
                Timestamp = DateTime.UtcNow,
                CorrelationId = Correlator.CurrentCorrelationId,
                Payload = JsonConvert.SerializeObject(job)
            };
        }
    }
}
