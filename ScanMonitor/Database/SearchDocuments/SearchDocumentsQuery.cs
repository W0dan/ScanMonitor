using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace ScanMonitor.Database.SearchDocuments
{
    public class SearchDocumentsQuery
    {
        public static List<DocumentDto> List(SearchDocumentsRequest request)
        {
            var documentTypeFilter = "";
            var voorWieFilter = "";
            var correspondentFilter = "";
            var datumOntvangenFilter = "";
            var searchStringFilter = "";

            if (request.DocumentTypeId != null)
            {
                documentTypeFilter = $"AND dt.Id = '{request.DocumentTypeId}' ";
            }
            if (request.PersonId != null)
            {
                voorWieFilter = $"AND p.Id = {request.PersonId} ";
            }
            if (request.CorrespondentId != null)
            {
                correspondentFilter = $"AND c.Id = '{request.CorrespondentId}' ";
            }
            if (request.Datum.HasValue)
            {
                datumOntvangenFilter = $"AND d.Datum = '{request.Datum:yyyy-MM-dd}' ";
            }
            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                searchStringFilter = "AND (" +
                                     $"dt.Name like '%{request.SearchString}%' " +
                                     $"OR p.Name like '%{request.SearchString}%' " +
                                     $"OR c.Name like '%{request.SearchString}%' " +
                                     $"OR d.Description like '%{request.SearchString}%' " +
                                     ") ";
            }

            var query =
                "SELECT dt.Name as DocumentType, p.Name as VoorWie, c.Name as Correspondent, d.Datum as DatumOntvangen, d.Description as Beschrijving, s.FileName " +
                "FROM documents d, DocumentTypes dt, Correspondents c, People p, Scans s " +
                "WHERE d.DocumentTypeId = dt.Id " +
                "AND d.CorrespondentId = c.Id " +
                "AND d.PersonId = p.Id " +
                "AND s.DocumentId = d.Id " +
                $"{documentTypeFilter} " +
                $"{correspondentFilter} " +
                $"{voorWieFilter} " +
                $"{datumOntvangenFilter} " +
                $"{searchStringFilter}";

            using (var dbConnection = DatabaseHelper.GetConnection())
                return dbConnection.Query<DocumentDto>(query, request).ToList();
        }
    }
}