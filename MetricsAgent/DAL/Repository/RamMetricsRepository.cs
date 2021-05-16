using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Model;

namespace MetricsAgent.Repository
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private const string Table = "ram_metrics";
        
        private IDataConnector _connector;

        public RamMetricsRepository(IDataConnector connector)
        {
            _connector = connector;
        }

        public IList<RamMetric> GetAll()
        {
            throw new NotImplementedException();
        }

        public RamMetric GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(RamMetric item)
        {
            using var connection = new SQLiteConnection(_connector.GetStringConnection());
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

        public void Update(RamMetric item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<RamMetric> GetByPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(_connector.GetStringConnection());
            using var cmd = new SQLiteCommand(connection);

            cmd.Connection.Open();
            // прописываем в команду SQL запрос на получение всех данных из таблицы
            cmd.CommandText = $"SELECT id, value, time FROM {Table} WHERE time BETWEEN @fromTime AND @toTime;";
            cmd.Parameters.AddWithValue("@fromTime", fromTime.ToUnixTimeSeconds());
            cmd.Parameters.AddWithValue("@toTime", toTime.ToUnixTimeSeconds());
            cmd.Prepare();

            var returnList = new List<RamMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new RamMetric
                    {
                        Id = reader.GetInt64(0),
                        Value = reader.GetInt64(1),
                        // налету преобразуем прочитанные секунды в метку времени
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2))
                    });
                }
            }

            return returnList;
        }
    }
}