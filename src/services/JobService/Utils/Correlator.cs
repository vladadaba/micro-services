using System;
using System.Threading;
using Serilog.Context;

namespace JobService.Utils
{
    public static class Correlator
    {
        static AsyncLocal<string> CorrelationId = new AsyncLocal<string>();

        public static string CurrentCorrelationId => CorrelationId.Value ?? "null";

        public static IDisposable BeginCorrelationScope(string correlationId)
        {
            if (CorrelationId.Value != null)
            {
                throw new InvalidOperationException("Already in an operation");
            }
            
            CorrelationId.Value = correlationId;
            return new CorrelationScope(LogContext.PushProperty("CorrelationId", correlationId));
        }

        class CorrelationScope : IDisposable
        {
            readonly IDisposable _logContextPop;

            public CorrelationScope(IDisposable logContextPop)
            {
                _logContextPop = logContextPop ?? throw new ArgumentNullException(nameof(logContextPop));
            }

            public void Dispose() => _logContextPop.Dispose();
        }
    }
}
