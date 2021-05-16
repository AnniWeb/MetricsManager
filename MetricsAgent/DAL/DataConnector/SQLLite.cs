using System.Data.SQLite;
using MetricsAgent.DAL.Interfaces;

namespace MetricsAgent.DAL.DataConnector
{
    public class SQLLite : IDataConnector
    {
        private const string CONNECTION = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        
        public SQLLite()
        {
            var connection = new SQLiteConnection(CONNECTION);
            connection.Open();  
        }
        public string GetStringConnection()
        {
            return CONNECTION;
        }
    }
}