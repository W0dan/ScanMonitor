using System.Linq;
using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.GetDocumentForEdit
{
    public class GetDocumentForEditQuery
    {
        public static DocumentDto Get(GetDocumentForEditRequest request)
        {
            const string getDocumentQuery = "SELECT d.Id, dt.Name as DocumentType, PersonId, CorrespondentId, d.Datum as DatumOntvangen, d.Description as Beschrijving " +
                                            "FROM documents d, DocumentTypes dt " +
                                            "WHERE d.DocumentTypeId = dt.Id " +
                                            "AND d.Id=@Id";

            const string getScansQuery = "SELECT Id, Filename, Datum " +
                                         "FROM Scans " +
                                         "WHERE DocumentId=@Id";

            // ignore intValue -> put all numeric data into decimalValue !!
            const string getCustomFieldsQuery = ";with customFields as (" +
                                                "select dcf.DocumentId, dtcf.DocumentTypeId, dtcf.FieldName, dtcf.FieldType, dcf.StringValue, DecimalValue, BooleanValue, DateValue " +
                                                "from DocumentCustomFields dcf " +
                                                "full outer join DocumentTypeCustomFields dtcf on dcf.DocumentTypeCustomFieldId = dtcf.Id) " +
                                                "select FieldName, FieldType, StringValue, DecimalValue, BooleanValue, DateValue " +
                                                "from documents d " +
                                                "inner join customFields cf on cf.DocumentId = d.Id or(cf.DocumentId is null AND cf.DocumentTypeId = d.DocumentTypeId) " +
                                                "WHERE d.Id = @Id";

            using (var dbConnection = AppConfig.Connections.ScanDbConnection)
            {
                var document = dbConnection.Query<DocumentDto>(getDocumentQuery, request).Single();

                var scans = dbConnection.Query<ScanDto>(getScansQuery, document).ToList();
                var customFields = dbConnection.Query<CustomFieldDto>(getCustomFieldsQuery, document).ToList();

                document.Scans = scans;
                document.CustomFields = customFields;

                return document;
            }
        }
    }
}