using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.CustomFieldVerwijderen
{
    public class CustomFieldVerwijderenQuery
    {
        public static void Delete(CustomFieldVerwijderenCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string query = "DELETE FROM DocumentTypeCustomFields " +
                                     "WHERE Id=@Id";

                connection.Execute(query, command);
            }
        }
    }
}