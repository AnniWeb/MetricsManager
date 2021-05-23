using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgentsManager.DAL.Interfaces
{
    public interface IMetricModel <T>
    {
        public long Id { get; set; }
        
        [Required]
        public T Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
        
        
        [Required]
        public long AgentId { get; set; }
    }
}