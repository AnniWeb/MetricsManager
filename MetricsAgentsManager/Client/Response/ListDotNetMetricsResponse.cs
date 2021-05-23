using System.Collections.Generic;
using MetricsAgentsManager.Client.Interfaces;

namespace MetricsAgentsManager.Client.Response
{
    public class ListDotNetMetricsResponse : IListMetricsResponse<DotNetMetricResponse>
    {
        public IList<DotNetMetricResponse> Metrics { get; set; }
    }
}