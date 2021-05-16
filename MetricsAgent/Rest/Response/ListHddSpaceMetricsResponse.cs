using System.Collections.Generic;

namespace MetricsAgent.Rest.Response
{
    public class ListHddSpaceMetricsResponse
    {
        public List<HddSpaceMetricResponse> Metrics { get; set; }
    }
}