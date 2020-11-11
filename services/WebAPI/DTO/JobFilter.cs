using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebAPI.DTO
{
    public class JobFilter
    {
        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }
    }
}
