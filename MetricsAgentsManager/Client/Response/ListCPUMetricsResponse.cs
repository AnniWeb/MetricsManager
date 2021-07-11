using System.Collections.Generic;
using MetricsAgentsManager.Client.Interfaces;

namespace MetricsAgentsManager.Client.Response
{
    public class ListCPUMetricsResponse : IListMetricsResponse<CPUMetricResponse>
    {
        public IList<CPUMetricResponse> Metrics { get; set; }
    }
}