using System;
using System.ComponentModel.DataAnnotations;
using MetricsAgentsManager.DAL.Interfaces;

namespace MetricsAgentsManager.DAL.Model
{
    public class CpuMetric : IMetricModelInt
    {
        public long Id { get; set; }
        
        [Required]
        public int Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
        public long AgentId { get; set; }
    }
}