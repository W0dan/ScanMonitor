using Dapper;

namespace ScanMonitor.Database.CreateDocument
{
    public static class CreateDocumentQuery
    {
        public static void Insert(CreateDocumentCommand command)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                const string query = "INSERT INTO documents(Id, DocumentTypeId, PersonId, CorrespondentId, Datum, Description) " +
                                     "VALUES(@Id, @DocumentTypeId, @PersonId, @CorrespondentId, @Datum, @Description)";

                connection.Execute(query, command);
            }
        }
    }
}