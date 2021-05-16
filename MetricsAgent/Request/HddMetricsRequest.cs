using System;

namespace MetricsAgent.Request
{
    public class HddMetricsRequest
    {
        public long Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}