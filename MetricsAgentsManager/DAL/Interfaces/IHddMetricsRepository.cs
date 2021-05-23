using System;
using System.Collections.Generic;
using MetricsAgentsManager.DAL.Model;
using MetricsAgentsManager.Repository;

namespace MetricsAgentsManager.DAL.Interfaces
{
    public interface IHddMetricsRepository : IRepository<HddSpaceMetric>
    {
        IList<HddSpaceMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0);

        DateTimeOffset GetDateUpdate(long agentId);
    }
}