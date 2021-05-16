using System.Collections.Generic;

namespace MetricsAgent.Rest.Response
{
    public class ListNetworkMetricsResponse
    {
        public List<NetworkMetricResponse> Metrics { get; set; }
    }
}