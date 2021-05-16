using System;

namespace MetricsAgent.Rest.Request
{
    public class RamMetricRequest
    {
        public long Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}