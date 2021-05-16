using System;
using System.Collections.Generic;
using MetricsAgent.DAL.Model;
using MetricsAgent.Repository;

namespace MetricsAgent.DAL.Interfaces
{
    public interface ICPUMetricsRepository : IRepository<CpuMetric>
    {
        IList<CpuMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}