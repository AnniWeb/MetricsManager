using System;

namespace MetricsAgent.Rest.Request
{
    public class CPUMetricRequest
    {
        public int Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}