using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgentsManager.DAL.Interfaces;
using MetricsAgentsManager.DAL.Model;
using Microsoft.AspNetCore.Http;

namespace MetricsAgentsManager.Repository
{
    public class AgentsRepository :  IAgentsRepository
    {
        private const string Table = "agents";
        private readonly IDataConnector _connector;
        
        private readonly ICPUMetricsRepository _cpuMetricsRepository;
        private readonly IDotNetMetricsRepository _dotNetMetricsRepository;
        private readonly IHddMetricsRepository _hddMetricsRepository;
        private readonly INetworkMetricsRepository _networkMetricsRepository;
        private readonly IRamMetricsRepository _ramMetricsRepository;

        public AgentsRepository(IDataConnector connector, ICPUMetricsRepository cpuMetricsRepository, IDotNetMetricsRepository dotNetMetricsRepository, IHddMetricsRepository hddMetricsRepository, INetworkMetricsRepository networkMetricsRepository, IRamMetricsRepository ramMetricsRepository)
        {
            _connector = connector;
            _cpuMetricsRepository = cpuMetricsRepository;
            _dotNetMetricsRepository = dotNetMetricsRepository;
            _hddMetricsRepository = hddMetricsRepository;
            _networkMetricsRepository = networkMetricsRepository;
            _ramMetricsRepository = ramMetricsRepository;
        }
        
        public IList<AgentInfo> GetAll()
        {
            using var connection = new SQLiteConnection(_connector.GetStringConnection());
            using var cmd = new SQLiteCommand(connection);

            cmd.Connection.Open();
            cmd.CommandText = $"SELECT id, active, host FROM {Table};";
            cmd.Prepare();

            var returnList = new List<AgentInfo>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new AgentInfo()
                    {
                        Id = reader.GetInt64(0),
                        Active = reader.GetBoolean(1),
                        Host = reader.GetString(2)
                    });
                }
            }

            return returnList;
        }

        public AgentInfo GetById(int id)
        {
            using var connection = new SQLiteConnection(_connector.GetStringConnection());
            using var cmd = new SQLiteCommand(connection);

            cmd.Connection.Open();
            cmd.CommandText = $"SELECT id, active, host FROM {Table} WHERE id = @id;";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return new AgentInfo()
                    {
                        Id = reader.GetInt64(0),
                        Active = reader.GetBoolean(1),
                        Host = reader.GetString(2)
                    };
                }
            }

            return null;
        }

        public void Create(AgentInfo item)
        {
            using var connection = new SQLiteConnection(_connector.GetStringConnection());
            // создаем команду
            using var cmd = new SQLiteCommand(connection);
            
            cmd.Connection.Open();
            // прописываем в команду SQL запрос на вставку данных
            cmd.CommandText = $"INSERT INTO {Table} (host, active) VALUES(@host, @active)";

            cmd.Parameters.AddWithValue("@host", item.Host);
            cmd.Parameters.AddWithValue("@active", item.Active == true);
            
            // подготовка команды к выполнению
            cmd.Prepare();

            // выполнение команды
            cmd.ExecuteNonQuery();
        }

        public void Update(AgentInfo item)
        {
            using var connection = new SQLiteConnection(_connector.GetStringConnection());
            // создаем команду
            using var cmd = new SQLiteCommand(connection);
            
            cmd.Connection.Open();

            if (!(item.Id > 0))
            {
                throw new ArgumentException("Не передан ид изменяемого объекта");
            }
            
            // прописываем в команду SQL запрос на вставку данных
            cmd.CommandText = $"UPDATE {Table} SET host = @host AND active @active WHERE id = @id";

            cmd.Parameters.AddWithValue("@host", item.Host);
            cmd.Parameters.AddWithValue("@active", item.Active == true);
            cmd.Parameters.AddWithValue("@id", item.Id);
            
            // подготовка команды к выполнению
            cmd.Prepare();

            // выполнение команды
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<AgentInfo> GetActiveAgents()
        {
            using var connection = new SQLiteConnection(_connector.GetStringConnection());
            using var cmd = new SQLiteCommand(connection);

            cmd.Connection.Open();
            cmd.CommandText = $"SELECT id, active, host FROM {Table} WHERE active = @active;";
            cmd.Parameters.AddWithValue("@active", true);
            cmd.Prepare();

            var returnList = new List<AgentInfo>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new AgentInfo()
                    {
                        Id = reader.GetInt64(0),
                        Active = reader.GetBoolean(1),
                        Host = reader.GetString(2)
                    });
                }
            }

            return returnList;
        }

        public IList<CpuMetric> GetCpuMetricsByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0)
        {
            return _cpuMetricsRepository.GetByPeriod(fromTime, toTime, agentId);
        }

        public IList<NetworkMetric> GetNetworkMetricsByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0)
        {
            return _networkMetricsRepository.GetByPeriod(fromTime, toTime, agentId);
        }

        public IList<DotNetMetric> GetDotNetMetricsByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0)
        {
            return _dotNetMetricsRepository.GetByPeriod(fromTime, toTime, agentId);
        }

        public IList<HddSpaceMetric> GetHddSpaceMetricsByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0)
        {
            return _hddMetricsRepository.GetByPeriod(fromTime, toTime, agentId);
        }

        public IList<RamMetric> GetRamMetricsByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, long agentId = 0)
        {
            return _ramMetricsRepository.GetByPeriod(fromTime, toTime, agentId);
        }
    }
}