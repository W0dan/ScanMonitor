using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.DocumentTypeAanpassen
{
    public class DocumentTypeAanpassenQuery
    {
        public static void Update(DocumentTypeAanpassenCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string query = "UPDATE Correspondents " +
                                     "SET Name=@Name " +
                                     "WHERE Id=@Id";

                connection.Execute(query, command);
            }
        }
    }

    public class DocumentTypeAanpassenCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}