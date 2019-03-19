using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.CreateDocument
{
    public static class CreateDocumentQuery
    {
        public static void Insert(CreateDocumentCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string query = "INSERT INTO documents(Id, DocumentTypeId, PersonId, CorrespondentId, Datum, Description) " +
                                     "VALUES(@Id, @DocumentTypeId, @PersonId, @CorrespondentId, @Datum, @Description)";

                connection.Execute(query, command);
            }
        }
    }
}