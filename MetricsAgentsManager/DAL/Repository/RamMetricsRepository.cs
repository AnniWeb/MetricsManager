using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using MetricsAgentsManager.DAL.Interfaces;
using MetricsAgentsManager.DAL.Model;

namespace MetricsAgentsManager.Repository
{
    public class RamMetricsRepository : AMetricsRepository<RamMetric, long>, IRamMetricsRepository
    {
        private const string TABLE_NAME = "ram_metrics";
        private IDataConnector _connector;

        public RamMetricsRepository(IDataConnector connector) : base(connector, TABLE_NAME)
        {
            _connector = connector;
        }

        public void Create(RamMetric item)
        {
            CreateMetric(item);
        }

        public IList<RamMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0)
        {
            return GetListByPeriod(fromTime, toTime, reader => new RamMetric
            {
                Id = reader.GetInt64(0),
                Value = reader.GetInt32(1),
                // налету преобразуем прочитанные секунды в метку времени
                Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2)),
                AgentId = reader.GetInt64(3)
            }, agentId);
        }

        public IList<RamMetric> GetAll()
        {
            throw new NotImplementedException();
        }

        public RamMetric GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(RamMetric item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}