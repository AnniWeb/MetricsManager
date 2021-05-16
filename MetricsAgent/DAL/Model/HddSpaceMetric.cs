using System;
using System.ComponentModel.DataAnnotations;
using MetricsAgent.DAL.Interfaces;

namespace MetricsAgent.DAL.Model
{
    public class HddSpaceMetric : IMetricModel<long>
    {
        public long Id { get; set; }
        
        [Required]
        public long Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}