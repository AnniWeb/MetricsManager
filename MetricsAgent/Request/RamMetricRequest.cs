using System;

namespace MetricsAgent.Request
{
    public class RamMetricRequest
    {
        public long Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}