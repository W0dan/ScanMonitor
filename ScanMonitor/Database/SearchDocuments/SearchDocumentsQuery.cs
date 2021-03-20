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

            var query = "with customFields as " + "\n" +
                "(" + "\n" +
                "SELECT d.Id as documentId," + "\n" +
                "STRING_AGG(" + "\n" +
                "dtcf.FieldName + '=' +" + "\n" +
                "case when dtcf.FieldType = 'JaNee' and dcf.BooleanValue = 1 then 'ja' " + "\n" +
                "when dtcf.FieldType = 'JaNee' and dcf.BooleanValue != 1 then 'nee' " + "\n" +
                "when dtcf.FieldType = 'Datum' then convert(varchar, dcf.DateValue) " + "\n" +
                "when dtcf.FieldType = 'Numeriek' then cast(dcf.DecimalValue as varchar) " + "\n" +
                "else dcf.StringValue end, ', ') as Value " + "\n" +
                "FROM documents d " + "\n" +
                "left outer join DocumentTypeCustomFields dtcf on dtcf.DocumentTypeId = d.DocumentTypeId " + "\n" +
                "left outer join DocumentCustomFields dcf on dcf.DocumentTypeCustomFieldId = dtcf.id and dcf.DocumentId = d.Id " + "\n" +
                "group by d.Id)," + "\n" +
                "documentscans as (" + "\n" +
                "SELECT d.Id as documentId, " + "\n" +
                "STRING_AGG(s.Filename, ',') as Files " + "\n" +
                "FROM documents d " + "\n" +
                "inner join Scans s on s.DocumentId = d.id " + "\n" +
                "group by d.Id), " + "\n" +
                "documentsJoined as (" + "\n" +
                "select d.Id, dt.Name as DocumentType, p.Name as VoorWie, c.Name as Correspondent, d.Datum as DatumOntvangen, trim(coalesce(d.Description, '') + ' ' + coalesce(cf.Value, '')) as Beschrijving, s.Files\n" +
                ",c.Id as CorrespondentId, p.Id as PersonId, dt.Id as DocumentTypeId\n" +
                "from documents d " + "\n" +
                "inner join DocumentTypes dt on d.DocumentTypeId = dt.Id " + "\n" +
                "inner join Correspondents c on d.CorrespondentId = c.Id " + "\n" +
                "inner join People p on d.PersonId = p.Id " + "\n" +
                "inner join documentscans s on s.DocumentId = d.Id " + "\n" +
                "left outer join customFields cf on cf.documentId = d.Id) " + "\n" +
                "SELECT Id, DocumentType, VoorWie, Correspondent, DatumOntvangen, Beschrijving, Files " + "\n" +
                "FROM documentsJoined " + "\n" +
                "WHERE 1=1 " + "\n" +
                $"{documentTypeFilter} " + "\n" +
                $"{correspondentFilter} " + "\n" +
                $"{voorWieFilter} " + "\n" +
                $"{datumOntvangenFilter} " + "\n" +
                $"{searchStringFilter}";

            using (var dbConnection = AppConfig.Connections.ScanDbConnection)
                return dbConnection.Query<DocumentDto>(query, request).ToList();
        }
    }
}