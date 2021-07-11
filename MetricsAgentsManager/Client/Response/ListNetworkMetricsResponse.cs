using System.Collections.Generic;
using MetricsAgentsManager.Client.Interfaces;

namespace MetricsAgentsManager.Client.Response
{
    public class ListNetworkMetricsResponse : IListMetricsResponse<NetworkMetricResponse>
    {
        public IList<NetworkMetricResponse> Metrics { get; set; }
    }
}