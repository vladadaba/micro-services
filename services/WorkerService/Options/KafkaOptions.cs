using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerService.Options
{
    public class KafkaOptions
    {
        public string BootstrapServers { get; set; } = "";
        public string JobRequestsTopic { get; set; } = "";
    }
}
