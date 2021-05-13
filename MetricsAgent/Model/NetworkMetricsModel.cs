using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Model
{
    public class NetworkMetricsModel : IMetricModel<string>
    {
        public int Id { get; set; }
        
        [Required]
        public string Value { get; set; }
        
        public DateTime DateTime { get; set; }
    }
}