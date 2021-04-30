using System.Collections.Generic;

namespace MetricsAgent.Response
{
    public class ListDotNetMetricsResponse
    {
        public List<DotNetMetricResponse> Metrics { get; set; }
    }
}