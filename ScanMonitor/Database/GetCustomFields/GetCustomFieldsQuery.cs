using System.Collections.Generic;
using System.Linq;
using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.GetCustomFields
{
    public class GetCustomFieldsQuery
    {
        public static List<CustomFieldDto> Get(GetCustomFieldsRequest request)
        {
            const string getCustomFieldsQuery = "select null as Id, Id as DocumentTypeCustomFieldId, FieldName, FieldType " +
                                                "from DocumentTypeCustomFields " +
                                                "where DocumentTypeId = @DocumentTypeId";

            using (var dbConnection = AppConfig.Connections.ScanDbConnection)
            {
                var customFields = dbConnection.Query<CustomFieldDto>(getCustomFieldsQuery, new
                {
                    request.DocumentTypeId
                }).ToList();

                return customFields;
            }
        }
    }
}