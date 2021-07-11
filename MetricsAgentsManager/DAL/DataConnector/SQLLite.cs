using System.Data.SQLite;
using MetricsAgentsManager.DAL.Interfaces;

namespace MetricsAgentsManager.DAL.DataConnector
{
    public class SQLLite : IDataConnector
    {
        private const string CONNECTION = "Data Source=manager.db;Version=3;Pooling=true;Max Pool Size=100;";
        
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