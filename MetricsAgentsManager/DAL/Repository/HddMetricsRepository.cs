
using System;
using System.Collections.Generic;
using MetricsAgentsManager.DAL.Interfaces;
using MetricsAgentsManager.DAL.Model;
using MetricsAgentsManager.Repository;

namespace MetricsAgentsManager.Repository
{
    public class HddMetricsRepository : AMetricsRepository<HddSpaceMetric, long>, IHddMetricsRepository
    {
        private const string TABLE_NAME = "hdd_metrics";
        
        private IDataConnector _connector;

        public HddMetricsRepository(IDataConnector connector) : base(connector, TABLE_NAME)
        {
            _connector = connector;
        }

        public void Create(HddSpaceMetric item)
        {
            CreateMetric(item);
        }

        public IList<HddSpaceMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0)
        {
            return GetListByPeriod(fromTime, toTime, reader => new HddSpaceMetric
            {
                Id = reader.GetInt64(0),
                Value = reader.GetInt32(1),
                // налету преобразуем прочитанные секунды в метку времени
                Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2)),
                AgentId = reader.GetInt64(3)
            }, agentId);
        }

        public IList<HddSpaceMetric> GetAll()
        {
            throw new NotImplementedException();
        }

        public HddSpaceMetric GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(HddSpaceMetric item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}