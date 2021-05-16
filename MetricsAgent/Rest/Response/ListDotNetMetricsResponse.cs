using System.Collections.Generic;

namespace MetricsAgent.Rest.Response
{
    public class ListDotNetMetricsResponse
    {
        public List<DotNetMetricResponse> Metrics { get; set; }
    }
}