using System;
using System.Collections.Generic;
using MetricsAgentsManager.DAL.Model;
using MetricsAgentsManager.Repository;

namespace MetricsAgentsManager.DAL.Interfaces
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {
        IList<RamMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0);

        DateTimeOffset GetDateUpdate(long agentId);
    }
}