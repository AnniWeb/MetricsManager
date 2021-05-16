using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Rest.Response
{
    public class DotNetMetricResponse
    {
        public int Id { get; set; }
        
        public string Value { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}