using System.Data;
using MySql.Data.MySqlClient;

namespace ScanMonitor.Database
{
    public static class DatabaseHelper
    {
        public static IDbConnection GetConnection()
        {
            return new MySqlConnection("server=ubuntu-nas;port=3306;database=scan;user=scanuser;password=scanuserpw");
        }
    }
}