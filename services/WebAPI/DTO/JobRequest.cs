using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.DTO
{
    public class JobRequest
    {
        public JobRequest()
        {

        }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
