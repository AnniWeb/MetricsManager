using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Model
{
    public class RamMetricsModel : IMetricModel<long>
    {
        public int Id { get; set; }
        
        [Required]
        public long Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}