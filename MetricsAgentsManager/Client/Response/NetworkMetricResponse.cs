using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgentsManager.Client.Response
{
    public class NetworkMetricResponse
    {
        public int Id { get; set; }
        
        public int Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}