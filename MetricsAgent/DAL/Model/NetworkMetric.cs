using System;
using System.ComponentModel.DataAnnotations;
using MetricsAgent.DAL.Interfaces;

namespace MetricsAgent.DAL.Model
{
    public class NetworkMetric : IMetricModel<int>
    {
        public long Id { get; set; }
        
        [Required]
        public int Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}