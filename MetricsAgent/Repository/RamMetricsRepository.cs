using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using MetricsAgent.Entity;
using MetricsAgent.Model;

namespace MetricsAgent.Repository
{
    public interface IRamMetricsRepository : IRepository<RamMetricsModel>, IMetricsRepository
    {
        IList<RamMetricsModel> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
        RamMetricsModel GetLast();
    }
    
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private const string Table = "ram_metrics";
        
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public IList<RamMetricsModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public RamMetricsModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(RamMetricsModel item)
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

        public void Update(RamMetricsModel item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<RamMetricsModel> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            using var cmd = new SQLiteCommand(connection);

            cmd.Connection.Open();
            // прописываем в команду SQL запрос на получение всех данных из таблицы
            cmd.CommandText = $"SELECT id, value, time FROM {Table} WHERE time BETWEEN @fromTime AND @toTime;";
            cmd.Parameters.AddWithValue("@fromTime", fromTime.ToUnixTimeSeconds());
            cmd.Parameters.AddWithValue("@toTime", toTime.ToUnixTimeSeconds());
            cmd.Prepare();

            var returnList = new List<RamMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new RamMetricsModel
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt64(1),
                        // налету преобразуем прочитанные секунды в метку времени
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2))
                    });
                }
            }

            return returnList;
        }

        public RamMetricsModel GetLast()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            using var cmd = new SQLiteCommand(connection);

            cmd.Connection.Open();
            // прописываем в команду SQL запрос на получение всех данных из таблицы
            cmd.CommandText = $"SELECT id, value, time FROM {Table} ORDER BY time DESC LIMIT 1;";

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                if (reader.Read())
                {
                    var time = reader.GetInt64(2);
                    // добавляем объект в список возврата
                    return new RamMetricsModel
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt64(1),
                        // налету преобразуем прочитанные секунды в метку времени
                        Time = DateTimeOffset.FromUnixTimeSeconds(time)
                    };
                }                
                else
                {
                    return null;
                }
            }
        }

        public void CreateTable()
        {
            using var conn = new SQLiteConnection(ConnectionString);
            using var command = new SQLiteCommand(conn);
            
            command.Connection.Open();
            command.CommandText = @$"CREATE TABLE IF NOT EXISTS {Table} (id INTEGER PRIMARY KEY, value INT, time INT);";
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