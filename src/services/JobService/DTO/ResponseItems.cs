using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JobService.DTO
{
    public class ResponseItems<T>
    {
        public ResponseItems(IEnumerable<T> items, int? nextCursor, int pageSize)
        {
            Items = items;
            NextCursor = nextCursor;
            PageSize = pageSize;
        }

        [JsonProperty(PropertyName = "items")]
        public IEnumerable<T> Items { get; set; }

        [JsonProperty(PropertyName = "next_cursor")]
        public int? NextCursor { get; set; }

        [JsonProperty(PropertyName = "page_size")]
        public int PageSize { get; set; }

    }
}
