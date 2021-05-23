using System.Collections.Generic;
using MetricsAgent.Rest.Interfaces;

namespace MetricsAgent.Rest.Response
{
    public class ListRamMetricsResponse : IListMetricsResponse<RamMetricResponse>
    {
        public IList<RamMetricResponse> Metrics { get; set; }
    }
}