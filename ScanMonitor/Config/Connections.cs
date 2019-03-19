using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace ScanMonitor.Config
{
    public class Connections
    {
        public ConnectionCompatibility ConnectionCompatibility { get; private set; }

        public IDbConnection ScanDbConnection
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ScanDbSqlConnection"];

                if (connectionString != null)
                {
                    ConnectionCompatibility = ConnectionCompatibility.SqlServer;
                    return new SqlConnection(connectionString.ConnectionString);
                }

                connectionString = ConfigurationManager.ConnectionStrings["ScanDbMySql"];
                if (connectionString != null)
                {
                    ConnectionCompatibility = ConnectionCompatibility.MySql;
                    return new MySqlConnection(connectionString.ConnectionString);
                }

                return null;
            }
        }
    }

    public enum ConnectionCompatibility
    {
        SqlServer = 1,
        MySql = 2
    }
}