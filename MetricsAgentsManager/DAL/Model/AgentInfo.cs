using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace MetricsAgentsManager.DAL.Model
{
    public class AgentInfo
    {
        public long Id { get; set; }

        [Required]
        public string Host { get; set; }
        
        public bool Active { get; set; }
    }
}