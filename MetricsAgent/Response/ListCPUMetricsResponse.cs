using System.Collections.Generic;

namespace MetricsAgent.Response
{
    public class ListCPUMetricsResponse
    {
        public List<CPUMetricResponse> Metrics { get; set; }
    }
}