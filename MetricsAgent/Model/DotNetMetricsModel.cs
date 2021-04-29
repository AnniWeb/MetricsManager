using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Model
{
    public class DotNetMetricsModel : IMetricModel<string>
    {
        public int Id { get; set; }
        
        [Required]
        public string Value { get; set; }
        
        public DateTime DateTime { get; set; }
    }
}