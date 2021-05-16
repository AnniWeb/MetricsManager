using System;

namespace MetricsAgent.Rest.Request
{
    public class NetworkMetricRequest
    {
        public int Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}