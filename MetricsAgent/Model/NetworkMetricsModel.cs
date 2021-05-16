using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Model
{
    public class NetworkMetricsModel : IMetricModel<int>
    {
        public int Id { get; set; }
        
        [Required]
        public int Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}