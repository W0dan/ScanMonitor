using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.DocumentTypeVerwijderen
{
    public class DocumentTypeVerwijderenQuery
    {
        public static void Delete(DocumentTypeVerwijderenCommand command)
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