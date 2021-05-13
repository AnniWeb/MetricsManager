using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Model
{
    public class CPUMetricsModel : IMetricModel<int>
    {
        public int Id { get; set; }
        
        [Required]
        public int Value { get; set; }
        
        public DateTime DateTime { get; set; }
    }
}