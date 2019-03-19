using System.Linq;
using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.GetDocumentFolderInfo
{
    public static class GetDocumentFolderInfoQuery
    {
        public static DocumentDto Get(string id)
        {
            const string query = "SELECT p.Name as Person, dt.Name as DocumentType, c.Name as Correspondent, d.Datum " +
                                 "FROM documents d, People p, DocumentTypes dt, Correspondents c " +
                                 "WHERE d.PersonId = p.Id " +
                                 "AND d.DocumentTypeId = dt.Id " +
                                 "AND d.CorrespondentId = c.Id " +
                                 "AND d.Id = @id";

            using (var dbConnection = AppConfig.Connections.ScanDbConnection)
            {
                return dbConnection.Query<DocumentDto>(query, new { id }).Single();
            }
        }
    }
}