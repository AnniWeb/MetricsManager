using System;

namespace MetricsAgent.Rest.Request
{
    public class DotNetMetricRequest
    {
        public string Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}