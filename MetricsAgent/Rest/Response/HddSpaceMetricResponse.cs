using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Rest.Response
{
    public class HddSpaceMetricResponse
    {
        public int Id { get; set; }
        
        public long Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}