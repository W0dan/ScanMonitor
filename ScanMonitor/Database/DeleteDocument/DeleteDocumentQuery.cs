using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.DeleteDocument
{
    public class DeleteDocumentQuery
    {
        public static void Insert(DeleteDocumentCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string query = "DELETE FROM documents " +
                                     "WHERE Id = @Id";

                connection.Execute(query, command);
            }
        }
    }
}