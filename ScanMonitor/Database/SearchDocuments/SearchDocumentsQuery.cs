using System.Collections.Generic;
using System.Linq;
using Dapper;
using ScanMonitor.Config;

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
                documentTypeFilter = $"AND DocumentTypeId = '{request.DocumentTypeId}' ";
            }
            if (request.PersonId != null)
            {
                voorWieFilter = $"AND PersonId = '{request.PersonId}' ";
            }
            if (request.CorrespondentId != null)
            {
                correspondentFilter = $"AND CorrespondentId = '{request.CorrespondentId}' ";
            }
            if (request.Datum.HasValue)
            {
                datumOntvangenFilter = $"AND DatumOntvangen = '{request.Datum:yyyy-MM-dd}' ";
            }
            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                searchStringFilter = "AND (" +
                                     $"DocumentType like '%{request.SearchString}%' " +
                                     $"OR VoorWie like '%{request.SearchString}%' " +
                                     $"OR Correspondent like '%{request.SearchString}%' " +
                                     $"OR Beschrijving like '%{request.SearchString}%' " +
                                     ") ";
            }

            var query = "with customFields as " +
                "(" +
                "SELECT d.Id as documentId," +
                "STRING_AGG(" +
                "dtcf.FieldName + '=' +" +
                "case when dtcf.FieldType = 'JaNee' and dcf.BooleanValue = 1 then 'ja' " +
                "when dtcf.FieldType = 'JaNee' and dcf.BooleanValue != 1 then 'nee' " +
                "when dtcf.FieldType = 'Datum' then convert(varchar, dcf.DateValue) " +
                "when dtcf.FieldType = 'Numeriek' then cast(dcf.DecimalValue as varchar) " +
                "else dcf.StringValue end, ', ') as Value " +
                "FROM documents d " +
                "left outer join DocumentTypeCustomFields dtcf on dtcf.DocumentTypeId = d.DocumentTypeId " +
                "left outer join DocumentCustomFields dcf on dcf.DocumentTypeCustomFieldId = dtcf.id and dcf.DocumentId = d.Id " +
                "group by d.Id)," +
                "documentscans as (" +
                "SELECT d.Id as documentId, " +
                "STRING_AGG(s.Filename, ',') as Files " +
                "FROM documents d " +
                "left outer join Scans s on s.DocumentId = d.id " +
                "group by d.Id), " +
                "documentsJoined as (" +
                "select d.Id, dt.Name as DocumentType, p.Name as VoorWie, c.Name as Correspondent, d.Datum as DatumOntvangen, trim(coalesce(d.Description, '') + ' ' + coalesce(cf.Value, '')) as Beschrijving, s.Files\n" +
                ",c.Id as CorrespondentId, p.Id as PersonId, dt.Id as DocumentTypeId\n" + 
                "from documents d " +
                "inner join DocumentTypes dt on d.DocumentTypeId = dt.Id " +
                "inner join Correspondents c on d.CorrespondentId = c.Id " +
                "inner join People p on d.PersonId = p.Id " +
                "inner join documentscans s on s.DocumentId = d.Id " +
                "left outer join customFields cf on cf.documentId = d.Id) " +
                "SELECT Id, DocumentType, VoorWie, Correspondent, DatumOntvangen, Beschrijving, Files " +
                "FROM documentsJoined " +
                "WHERE 1=1 " +
                $"{documentTypeFilter} " +
                $"{correspondentFilter} " +
                $"{voorWieFilter} " +
                $"{datumOntvangenFilter} " +
                $"{searchStringFilter}";

            using (var dbConnection = AppConfig.Connections.ScanDbConnection)
                return dbConnection.Query<DocumentDto>(query, request).ToList();
        }
    }
}