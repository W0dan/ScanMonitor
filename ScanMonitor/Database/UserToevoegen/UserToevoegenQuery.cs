using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.UserToevoegen
{
    public class UserToevoegenQuery
    {
        public static void Insert(UserToevoegenCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string query = "INSERT INTO People(Id, Name) " +
                                     "VALUES(@Id, @Name)";

                connection.Execute(query, command);
            }
        }
    }
}