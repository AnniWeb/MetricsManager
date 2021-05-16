using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Rest.Response
{
    public class NetworkMetricResponse
    {
        public int Id { get; set; }
        
        public int Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}