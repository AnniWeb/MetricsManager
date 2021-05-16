using System;

namespace MetricsAgent.Rest.Request
{
    public class HddMetricRequest
    {
        public long Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}