using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Model
{
    public interface IMetricModel <T>
    {
        public int Id { get; set; }
        
        [Required]
        public T Value { get; set; }
        
        public DateTime DateTime { get; set; }
    }
}