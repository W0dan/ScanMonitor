using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.CreateScan
{
    public static class CreateScanQuery
    {
        public static void Insert(CreateScanCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string query = "INSERT INTO Scans(Id, DocumentId, Filename, Datum) " +
                                     "VALUES(@Id, @DocumentId, @Filename, @Datum)";

                connection.Execute(query, command);
            }
        }
    }
}