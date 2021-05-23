using System.Collections.Generic;
using MetricsAgent.Rest.Interfaces;

namespace MetricsAgent.Rest.Response
{
    public class ListCPUMetricsResponse : IListMetricsResponse<CPUMetricResponse>
    {
        public IList<CPUMetricResponse> Metrics { get; set; }
    }
}