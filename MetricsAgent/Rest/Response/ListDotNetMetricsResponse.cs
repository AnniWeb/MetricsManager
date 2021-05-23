using System.Collections.Generic;
using MetricsAgent.Rest.Interfaces;

namespace MetricsAgent.Rest.Response
{
    public class ListDotNetMetricsResponse : IListMetricsResponse<DotNetMetricResponse>
    {
        public IList<DotNetMetricResponse> Metrics { get; set; }
    }
}