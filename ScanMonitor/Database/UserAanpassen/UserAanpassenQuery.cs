using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.UserAanpassen
{
    public class UserAanpassenQuery
    {
        public static void Update(UserAanpassenCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string query = "UPDATE People " +
                                     "SET Name=@Name " +
                                     "WHERE Id=@Id";

                connection.Execute(query, command);
            }
        }
    }
}