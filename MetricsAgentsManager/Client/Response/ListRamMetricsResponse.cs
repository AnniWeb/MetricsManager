using System.Collections.Generic;
using MetricsAgentsManager.Client.Interfaces;

namespace MetricsAgentsManager.Client.Response
{
    public class ListRamMetricsResponse : IListMetricsResponse<RamMetricResponse>
    {
        public IList<RamMetricResponse> Metrics { get; set; }
    }
}