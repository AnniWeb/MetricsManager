using System;

namespace MetricsAgentsManager.Client.Response
{
    public class CPUMetricResponse
    {
        public int Id { get; set; }
        
        public int Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}