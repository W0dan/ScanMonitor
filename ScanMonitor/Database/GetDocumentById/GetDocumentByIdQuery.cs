using System.Linq;
using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.GetDocumentById
{
    public class GetDocumentByIdQuery
    {
        public static DocumentDto Get(GetDocumentByIdRequest request)
        {
            const string getDocumentQuery = "SELECT d.Id, dt.Name as DocumentType, p.Name as VoorWie, c.Name as Correspondent, d.Datum as DatumOntvangen, d.Description as Beschrijving " +
                                            "FROM documents d, DocumentTypes dt, Correspondents c, People p " +
                                            "WHERE d.DocumentTypeId = dt.Id " +
                                            "AND d.CorrespondentId = c.Id " +
                                            "AND d.PersonId = p.Id " +
                                            "AND d.Id=@Id";

            const string getScansQuery = "SELECT Id, Filename, Datum " +
                                         "FROM Scans " +
                                         "WHERE DocumentId=@Id";

            using (var dbConnection = AppConfig.Connections.ScanDbConnection)
            {
                var document = dbConnection.Query<DocumentDto>(getDocumentQuery, request).Single();

                var scans = dbConnection.Query<ScanDto>(getScansQuery, document).ToList();

                document.Scans = scans;

                return document;
            }
        }
    }
}