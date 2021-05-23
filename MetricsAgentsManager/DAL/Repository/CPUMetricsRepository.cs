using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using MetricsAgentsManager.DAL.Interfaces;
using MetricsAgentsManager.DAL.Model;

namespace MetricsAgentsManager.Repository
{
    public class CPUMetricsRepository : AMetricsRepository<CpuMetric, int>, ICPUMetricsRepository
    {
        private const string TABLE_NAME = "cpu_metrics";
        private IDataConnector _connector;

        public CPUMetricsRepository(IDataConnector connector) : base(connector, TABLE_NAME)
        {
            _connector = connector;
        }
        
        public void Create(CpuMetric item)
        {
            CreateMetric(item);
        }
        
        public IList<CpuMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0)
        {
            return GetListByPeriod(fromTime, toTime, reader => new CpuMetric
            {
                Id = reader.GetInt64(0),
                Value = reader.GetInt32(1),
                Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2)),
                AgentId = reader.GetInt64(3)
            }, agentId);
        }
        
        public IList<CpuMetric> GetAll()
        {
            throw new NotImplementedException();
        }

        public CpuMetric GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CpuMetric item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}