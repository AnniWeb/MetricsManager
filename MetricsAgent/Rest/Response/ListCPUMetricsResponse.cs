using System.Collections.Generic;

namespace MetricsAgent.Rest.Response
{
    public class ListCPUMetricsResponse
    {
        public List<CPUMetricResponse> Metrics { get; set; }
    }
}