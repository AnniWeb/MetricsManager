using System.Collections.Generic;
using MetricsAgent.Rest.Interfaces;

namespace MetricsAgent.Rest.Response
{
    public class ListHddSpaceMetricsResponse : IListMetricsResponse<HddSpaceMetricResponse>
    {
        public IList<HddSpaceMetricResponse> Metrics { get; set; }
    }
}