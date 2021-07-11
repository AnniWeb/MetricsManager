using System;
using System.Collections.Generic;
using MetricsAgentsManager.DAL.Model;
using MetricsAgentsManager.Repository;

namespace MetricsAgentsManager.DAL.Interfaces
{
    public interface IAgentsRepository : IRepository<AgentInfo>
    {
        IList<AgentInfo> GetActiveAgents();
        IList<CpuMetric> GetCpuMetricsByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0);
        IList<NetworkMetric> GetNetworkMetricsByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0);
        IList<DotNetMetric> GetDotNetMetricsByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0);
        IList<HddSpaceMetric> GetHddSpaceMetricsByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0);
        IList<RamMetric> GetRamMetricsByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0);
    }
}