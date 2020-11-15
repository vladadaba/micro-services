using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JobService.DTO
{
    public class JobFilter
    {
        private const int MAX_PAGE_SIZE = 50;
        private const int MIN_PAGE_SIZE = 10;
        private const int DEFAULT_PAGE_SIZE = 20;

        private int _pageSize;

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "next_cursor")]
        public int? NextCursor { get; set; }

        [JsonProperty(PropertyName = "page_size", DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue(DEFAULT_PAGE_SIZE)]
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = Math.Max(Math.Min(value, MAX_PAGE_SIZE), MIN_PAGE_SIZE); }
        }
    }
}
