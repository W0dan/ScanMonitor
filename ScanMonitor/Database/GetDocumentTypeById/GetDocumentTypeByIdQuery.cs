using System.Linq;
using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.GetDocumentTypeById
{
    public class GetDocumentTypeByIdQuery
    {
        public static DocumentTypeDto Get(GetDocumentTypeByIdRequest request)
        {
            string getDocumentTypeByIdQuery = "SELECT Id, Name " +
                                              "FROM DocumentTypes " +
                                              "WHERE Id=@Id";

            string getCustomFieldsQuery = "SELECT Id, DocumentTypeId, FieldName, FieldType, Mandatory " +
                                          "FROM DocumentTypeCustomFields " +
                                          "WHERE DocumentTypeId=@Id";

            using (var dbConnection = AppConfig.Connections.ScanDbConnection)
            {
                var documentType = dbConnection.Query<DocumentTypeDto>(getDocumentTypeByIdQuery, request).Single();

                var customFields = dbConnection.Query<CustomFieldDto>(getCustomFieldsQuery, documentType).ToList();

                documentType.CustomFields = customFields;

                return documentType;
            }
        }
    }
}