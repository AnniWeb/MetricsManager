using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgentsManager.DAL.Interfaces;

namespace MetricsAgentsManager.Repository
{
    public abstract class AMetricsRepository<T, TV> where T : IMetricModel<TV>
    {
        private readonly IDataConnector _connector;
        protected readonly string _tableName;

        protected AMetricsRepository(IDataConnector connector, string tableName)
        {
            _connector = connector;
            _tableName = tableName;
        }

        /// <summary>
        /// Сохранение метрики в БД
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected void CreateMetric(T item)
        {
            using var connection = new SQLiteConnection(_connector.GetStringConnection());
            using var cmd = new SQLiteCommand(connection);
            
            cmd.Connection.Open();
            cmd.CommandText = $"INSERT INTO {_tableName} (agentId, value, time) VALUES(@agent, @value, @time)";

            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@agent", item.AgentId);
            cmd.Parameters.AddWithValue("@time", item.Time.ToUnixTimeSeconds());
            
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        
        protected IList<T> GetListByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, Func<SQLiteDataReader, T> mapList, long agentId = 0)
        {
            using var connection = new SQLiteConnection(_connector.GetStringConnection());
            using var cmd = new SQLiteCommand(connection);

            cmd.Connection.Open();
            // прописываем в команду SQL запрос на получение всех данных из таблицы
            // cmd.CommandText = $"SELECT id, value, time FROM {Table} WHERE time BETWEEN @fromTime AND @toTime;";
            cmd.CommandText = agentId > 0 
                ? $"SELECT id, value, time, agentId FROM {_tableName} WHERE agentId = @agent;" 
                : $"SELECT id, value, time, agentId FROM {_tableName};";
            cmd.Parameters.AddWithValue("@fromTime", fromTime.ToUnixTimeSeconds());
            cmd.Parameters.AddWithValue("@toTime", toTime.ToUnixTimeSeconds());
            cmd.Parameters.AddWithValue("@agent", agentId);
            cmd.Prepare();

            var returnList = new List<T>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(mapList(reader));
                }
            }

            return returnList;
        }

        public DateTimeOffset GetDateUpdate(long agentId)
        {
            using var connection = new SQLiteConnection(_connector.GetStringConnection());
            using var cmd = new SQLiteCommand(connection);

            cmd.Connection.Open();
            cmd.CommandText = $"SELECT MAX(time) FROM {_tableName} WHERE agentId = @agent;";
            cmd.Parameters.AddWithValue("@agent", agentId);
            cmd.Prepare();
            long timestamp = 0;

            try
            {
                timestamp = (long) cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                
            }

            var returnDate = timestamp > 0 ? DateTimeOffset.FromUnixTimeSeconds(timestamp) : DateTimeOffset.MinValue;

            return returnDate;
        }
    }
}