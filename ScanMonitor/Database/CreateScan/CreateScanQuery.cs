using Dapper;

namespace ScanMonitor.Database.CreateScan
{
    public static class CreateScanQuery
    {
        public static void Insert(CreateScanCommand command)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                const string query = "INSERT INTO Scans(Id, DocumentId, Filename, Datum) " +
                                     "VALUES(@Id, @DocumentId, @Filename, @Datum)";

                connection.Execute(query, command);
            }
        }
    }
}