using System;

namespace MetricsAgent.Request
{
    public class DotNetMetricRequest
    {
        public string Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}