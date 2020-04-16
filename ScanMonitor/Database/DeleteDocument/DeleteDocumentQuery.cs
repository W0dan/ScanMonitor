using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.DeleteDocument
{
    public class DeleteDocumentQuery
    {
        public static void Delete(DeleteDocumentCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string deleteScansQuery = "DELETE FROM Scans " +
                                                "WHERE DocumentId = @Id";

                connection.Execute(deleteScansQuery, command);

                const string deleteCustomFieldsQuery = "DELETE FROM DocumentCustomFields " +
                                                       "WHERE DocumentId = @Id";

                connection.Execute(deleteCustomFieldsQuery, command);

                const string deleteDocumentQuery = "DELETE FROM documents " +
                                     "WHERE Id = @Id";

                connection.Execute(deleteDocumentQuery, command);
            }
        }
    }
}