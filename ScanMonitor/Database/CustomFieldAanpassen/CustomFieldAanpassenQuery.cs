using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.CustomFieldAanpassen
{
    public class CustomFieldAanpassenQuery
    {
        public static void Update(CustomFieldAanpassenCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string query = "UPDATE DocumentTypeCustomFields " +
                                     "SET FieldName=@FieldName, " +
                                     "FieldType=@FieldType, " +
                                     "Mandatory=@Mandatory " +
                                     "WHERE Id=@Id";

                connection.Execute(query, command);
            }
        }
    }
}