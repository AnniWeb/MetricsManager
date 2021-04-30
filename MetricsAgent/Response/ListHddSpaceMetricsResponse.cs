using System.Collections.Generic;

namespace MetricsAgent.Response
{
    public class ListHddSpaceMetricsResponse
    {
        public List<HddSpaceMetricResponse> Metrics { get; set; }
    }
}