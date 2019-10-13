using System.Linq;
using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.GetDocumentForEdit
{
    public class GetDocumentForEditQuery
    {
        public static DocumentDto Get(GetDocumentForEditRequest request)
        {
            const string getDocumentQuery = "SELECT d.Id, dt.Name as DocumentType, dt.Id as DocumentTypeId, PersonId, CorrespondentId, d.Datum as DatumOntvangen, d.Description as Beschrijving " +
                                            "FROM documents d, DocumentTypes dt " +
                                            "WHERE d.DocumentTypeId = dt.Id " +
                                            "AND d.Id=@Id";

            const string getScansQuery = "SELECT Id, Filename, Datum " +
                                         "FROM Scans " +
                                         "WHERE DocumentId=@Id";

            // ignore intValue -> put all numeric data into decimalValue !!
            const string getCustomFieldsQuery = ";with dtcf as ( " +
                                                "select * " +
                                                "from DocumentTypeCustomFields " +
                                                "where DocumentTypeId = @DocumentTypeId " +
                                                ") " +
                                                "select dcf.Id, coalesce(dtcf.Id, dtcfDef.Id) as DocumentTypeCustomFieldId, coalesce(dtcf.FieldName, dtcfDef.FieldName) as FieldName, coalesce(dtcf.FieldType, dtcfDef.FieldType) as FieldType, dcf.StringValue, dcf.DecimalValue as NumericValue, dcf.BooleanValue, dcf.DateValue " +
                                                "from Documents d " +
                                                "left outer join DocumentCustomFields dcf on dcf.DocumentId = d.id " +
                                                "left outer join dtcf on dcf.DocumentTypeCustomFieldId = dtcf.Id " +
                                                "left outer join dtcf as dtcfDef on d.DocumentTypeId = dtcfDef.DocumentTypeId and dcf.id is null " +
                                                "where d.Id = @DocumentId and not (dtcf.Id is null and dtcfDef.Id is null) ";

            using (var dbConnection = AppConfig.Connections.ScanDbConnection)
            {
                var document = dbConnection.Query<DocumentDto>(getDocumentQuery, request).Single();

                var scans = dbConnection.Query<ScanDto>(getScansQuery, document).ToList();
                var customFields = dbConnection.Query<CustomFieldDto>(getCustomFieldsQuery, new
                {
                    document.DocumentTypeId,
                    DocumentId = document.Id
                }).ToList();

                document.Scans = scans;
                document.CustomFields = customFields;

                return document;
            }
        }
    }
}