using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using MetricsAgentsManager.DAL.Interfaces;
using MetricsAgentsManager.DAL.Model;

namespace MetricsAgentsManager.Repository
{
    public class DotNetMetricsRepository : AMetricsRepository<DotNetMetric, int>, IDotNetMetricsRepository
    {
        private const string TABLE_NAME = "dot_net_metrics";
        private IDataConnector _connector;

        public DotNetMetricsRepository(IDataConnector connector) : base(connector, TABLE_NAME)
        {
            _connector = connector;
        }

        public void Create(DotNetMetric item)
        {
            CreateMetric(item);
        }

        public IList<DotNetMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0)
        {
            return GetListByPeriod(fromTime, toTime, reader => new DotNetMetric
            {
                Id = reader.GetInt64(0),
                Value = reader.GetInt32(1),
                // налету преобразуем прочитанные секунды в метку времени
                Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2)),
                AgentId = reader.GetInt64(3)
            }, agentId);
        }

        public IList<DotNetMetric> GetAll()
        {
            throw new NotImplementedException();
        }

        public DotNetMetric GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(DotNetMetric item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}