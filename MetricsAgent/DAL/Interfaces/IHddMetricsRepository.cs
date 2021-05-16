using System;
using System.Collections.Generic;
using MetricsAgent.DAL.Model;
using MetricsAgent.Repository;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IHddMetricsRepository : IRepository<HddSpaceMetric>
    {
        IList<HddSpaceMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}