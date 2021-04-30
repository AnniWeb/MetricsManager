using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Model
{
    public class HddSpaceMetricsModel : IMetricModel<long>
    {
        public int Id { get; set; }
        
        [Required]
        public long Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}