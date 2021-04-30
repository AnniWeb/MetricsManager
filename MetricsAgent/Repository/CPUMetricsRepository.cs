using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using MetricsAgent.Entity;
using MetricsAgent.Model;

namespace MetricsAgent.Repository
{
    public interface ICPUMetricsRepository : IRepository<CPUMetricsModel>, IMetricsRepository
    {
        IList<CPUMetricsModel> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
        IList<CPUMetricsModel> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, Percentile percentile);
    }
    
    public class CPUMetricsRepository : ICPUMetricsRepository
    {
        private const string Table = "cpu_metrics";
        
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public IList<CPUMetricsModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public CPUMetricsModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(CPUMetricsModel item)
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
            cmd.Parameters.AddWithValue("@time", item.Time.ToUnixTimeMilliseconds());
            // подготовка команды к выполнению
            cmd.Prepare();

            // выполнение команды
            cmd.ExecuteNonQuery();
        }

        public void Update(CPUMetricsModel item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<CPUMetricsModel> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            using var cmd = new SQLiteCommand(connection);

            cmd.Connection.Open();
            // прописываем в команду SQL запрос на получение всех данных из таблицы
            cmd.CommandText = $"SELECT id, value, time FROM {Table} WHERE time BETWEEN @fromTime AND @toTime;";
            cmd.Parameters.AddWithValue("@fromTime", fromTime.ToUnixTimeSeconds());
            cmd.Parameters.AddWithValue("@toTime", toTime.ToUnixTimeSeconds());
            cmd.Prepare();

            var returnList = new List<CPUMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new CPUMetricsModel
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

        public IList<CPUMetricsModel> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime, Percentile percentile)
        {
            throw new NotImplementedException();
        }

        public void CreateTable()
        {
            bool createTable = false;
            using var conn = new SQLiteConnection(ConnectionString);
            using var command = new SQLiteCommand(conn);
            
            command.Connection.Open();
            command.CommandText = @$"CREATE TABLE IF NOT EXISTS {Table} (id INTEGER PRIMARY KEY, value INT, time INT);";
            command.ExecuteNonQuery();
        }
        
        public void DropTable()
        {
            bool createTable = false;
            using var conn = new SQLiteConnection(ConnectionString);
            using var command = new SQLiteCommand(conn);
            
            command.Connection.Open();
            command.CommandText = @$"DROP TABLE IF EXISTS {Table};";
            command.ExecuteNonQuery();
        }
    }
}