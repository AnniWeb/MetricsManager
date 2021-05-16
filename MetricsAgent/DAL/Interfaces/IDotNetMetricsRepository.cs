using System;
using System.Collections.Generic;
using MetricsAgent.DAL.Model;
using MetricsAgent.Repository;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {
        IList<DotNetMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}