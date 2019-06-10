using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.CorrespondentVerwijderen
{
    public class CorrespondentVerwijderenQuery
    {
        public static void Delete(CorrespondentVerwijderenCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string query = "DELETE FROM Correspondents " +
                                     "WHERE Id=@Id";

                connection.Execute(query, command);
            }
        }
    }
}