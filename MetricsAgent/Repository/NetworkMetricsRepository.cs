using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using MetricsAgent.Entity;
using MetricsAgent.Model;

namespace MetricsAgent.Repository
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetricsModel>, IMetricsRepository
    {
        IList<NetworkMetricsModel> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
    
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private const string Table = "network_metrics";
        
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public IList<NetworkMetricsModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public NetworkMetricsModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(NetworkMetricsModel item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // создаем команду
            using var cmd = new SQLiteCommand(connection);
            
            cmd.Connection.Open();
            // прописываем в команду SQL запрос на вставку данных
            cmd.CommandText = $"INSERT INTO {Table} (value, time) VALUES(@value, @time)";

            // добавляем параметры в запрос из нашего объекта
            cmd.Parameters.AddWithValue("@value", item.Value);

            // в таблице будем хранить время в секундах, потому преобразуем перед записью в секунды
            // через свойство
            cmd.Parameters.AddWithValue("@time", item.Time.ToUnixTimeSeconds());
            // подготовка команды к выполнению
            cmd.Prepare();

            // выполнение команды
            cmd.ExecuteNonQuery();
        }

        public void Update(NetworkMetricsModel item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<NetworkMetricsModel> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            using var cmd = new SQLiteCommand(connection);

            cmd.Connection.Open();
            // прописываем в команду SQL запрос на получение всех данных из таблицы
            cmd.CommandText = $"SELECT id, value, time FROM {Table} WHERE time BETWEEN @fromTime AND @toTime;";
            cmd.Parameters.AddWithValue("@fromTime", fromTime.ToUnixTimeSeconds());
            cmd.Parameters.AddWithValue("@toTime", toTime.ToUnixTimeSeconds());
            cmd.Prepare();

            var returnList = new List<NetworkMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new NetworkMetricsModel
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        // налету преобразуем прочитанные секунды в метку времени
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2))
                    });
                }
            }

            return returnList;
        }

        public void CreateTable()
        {
            using var conn = new SQLiteConnection(ConnectionString);
            using var command = new SQLiteCommand(conn);
            
            command.Connection.Open();
            command.CommandText = @$"CREATE TABLE IF NOT EXISTS {Table} (id INTEGER PRIMARY KEY, value INT, time INT64);";
            command.ExecuteNonQuery();
        }
        
        public void DropTable()
        {
            using var conn = new SQLiteConnection(ConnectionString);
            using var command = new SQLiteCommand(conn);
            
            command.Connection.Open();
            command.CommandText = @$"DROP TABLE IF EXISTS {Table};";
            command.ExecuteNonQuery();
        }
    }
}