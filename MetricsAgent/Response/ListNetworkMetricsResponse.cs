using System.Collections.Generic;

namespace MetricsAgent.Response
{
    public class ListNetworkMetricsResponse
    {
        public List<NetworkMetricResponse> Metrics { get; set; }
    }
}