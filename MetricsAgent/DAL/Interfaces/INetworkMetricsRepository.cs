using System;
using System.Collections.Generic;
using MetricsAgent.DAL.Model;
using MetricsAgent.Repository;

namespace MetricsAgent.DAL.Interfaces
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {
        IList<NetworkMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}