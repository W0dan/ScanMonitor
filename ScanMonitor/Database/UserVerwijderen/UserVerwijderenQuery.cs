using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.UserVerwijderen
{
    public class UserVerwijderenQuery
    {
        public static void Delete(UserVerwijderenCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string query = "DELETE FROM People " +
                                     "WHERE Id=@Id";

                connection.Execute(query, command);
            }
        }
    }
}