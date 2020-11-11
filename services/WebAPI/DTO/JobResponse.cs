using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebAPI.Models;

namespace WebAPI.DTO
{
    public class JobResponse
    {
        public JobResponse()
        {

        }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = "";

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; } = "";

        public static JobResponse From(JobItem item)
        {
            return new JobResponse
            {
                Id = item.Id,
                Name = item.Name,
                Status = item.Status.ToString()
            };
        }
    }
}
