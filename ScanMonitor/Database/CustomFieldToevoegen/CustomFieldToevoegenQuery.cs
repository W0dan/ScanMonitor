using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.CustomFieldToevoegen
{
    public class CustomFieldToevoegenQuery
    {
        public static void Insert(CustomFieldToevoegenCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string query = "INSERT INTO DocumentTypeCustomFields(Id, DocumentTypeId, FieldName, FieldType, Mandatory) " +
                                     "VALUES(@Id, @DocumentTypeId, @FieldName, @FieldType, @Mandatory)";

                connection.Execute(query, command);
            }
        }
    }
}