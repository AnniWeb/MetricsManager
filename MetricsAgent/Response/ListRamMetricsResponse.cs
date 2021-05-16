using System.Collections.Generic;

namespace MetricsAgent.Response
{
    public class ListRamMetricsResponse
    {
        public List<RamMetricResponse> Metrics { get; set; }
    }
}