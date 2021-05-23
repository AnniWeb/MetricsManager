using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgentsManager.Rest.Response
{
    public partial class NetworkMetricResponse
    {
        public int Id { get; set; }
        
        public int Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}