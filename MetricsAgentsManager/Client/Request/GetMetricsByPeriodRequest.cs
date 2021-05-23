using System;
using Microsoft.AspNetCore.Http;

namespace MetricsAgentsManager.Client.Request
{
    public class GetMetricsByPeriodRequest
    {
        public string ApiHost { get; set; }
        
        public string ApiMethod { get; set; }
        public DateTimeOffset FromTime { get; set; } 
        public DateTimeOffset ToTime { get; set; } 
    }
}