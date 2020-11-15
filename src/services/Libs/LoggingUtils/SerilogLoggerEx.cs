using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace LoggingUtils
{
    public static class SerilogLoggerEx
    {
        public static void AddSerilogLogger(this IServiceCollection services, IConfiguration conf)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(conf)
                .CreateLogger();
            services.AddSingleton(Log.Logger);
        }
    }
}
