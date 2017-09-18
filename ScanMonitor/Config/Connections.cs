using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace ScanMonitor.Config
{
    public class Connections
    {
        public IDbConnection ScanDbMySql => new MySqlConnection(ConfigurationManager.ConnectionStrings[nameof(ScanDbMySql)].ConnectionString);
    }
}