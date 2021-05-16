using System;

namespace MetricsAgent.Request
{
    public class CPUMetricRequest
    {
        public int Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}