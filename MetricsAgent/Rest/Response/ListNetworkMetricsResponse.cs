using System.Collections.Generic;
using MetricsAgent.Rest.Interfaces;

namespace MetricsAgent.Rest.Response
{
    public class ListNetworkMetricsResponse : IListMetricsResponse<NetworkMetricResponse>
    {
        public IList<NetworkMetricResponse> Metrics { get; set; }
    }
}