using System;
using System.Collections.Generic;
using MetricsAgent.DAL.Model;
using MetricsAgent.Repository;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {
        IList<RamMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}