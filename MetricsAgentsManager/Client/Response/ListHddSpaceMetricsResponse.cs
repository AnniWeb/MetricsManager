using System.Collections.Generic;
using MetricsAgentsManager.Client.Interfaces;

namespace MetricsAgentsManager.Client.Response
{
    public class ListHddSpaceMetricsResponse : IListMetricsResponse<HddSpaceMetricResponse>
    {
        public IList<HddSpaceMetricResponse> Metrics { get; set; }
    }
}