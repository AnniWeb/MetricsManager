using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgentsManager.Model
{
    public class AgentInfo
    {
        public int AgentId { get; set; }

        [Required]
        public Uri AgentAddress { get; set; }
    }
}