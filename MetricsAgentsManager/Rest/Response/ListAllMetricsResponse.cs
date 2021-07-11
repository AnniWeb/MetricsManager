using System.Collections.Generic;

namespace MetricsAgentsManager.Rest.Response
{
    public class ListAllMetricsResponse
    {
        public IEnumerable<CPUMetricResponse> CpuMetrics { get; set; }
        public IEnumerable<NetworkMetricResponse> NetworkMetrics { get; set; }
        public IEnumerable<RamMetricResponse> RamMetrics { get; set; }
        public IEnumerable<DotNetMetricResponse> DotNetMetrics { get; set; }
        public IEnumerable<HddSpaceMetricResponse> HddSpaceMetrics { get; set; }
    }
}