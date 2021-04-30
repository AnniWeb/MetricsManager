using System;

namespace MetricsAgent.Request
{
    public class NetworkMetricRequest
    {
        public int Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}