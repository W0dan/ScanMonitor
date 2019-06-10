using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.CorrespondentAanpassen
{
    public class CorrespondentAanpassenQuery
    {
        public static void Update(CorrespondentAanpassenCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string query = "UPDATE Correspondents " +
                                     "SET Name=@Name " +
                                     "WHERE Id=@Id";

                connection.Execute(query, command);
            }
        }
    }
}