using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Response
{
    public class RamMetricResponse
    {
        public int Id { get; set; }
        
        public long Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}