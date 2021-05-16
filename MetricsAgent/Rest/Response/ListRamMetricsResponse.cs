using System.Collections.Generic;

namespace MetricsAgent.Rest.Response
{
    public class ListRamMetricsResponse
    {
        public List<RamMetricResponse> Metrics { get; set; }
    }
}