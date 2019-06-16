using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.SaveDocument
{
    public class SaveDocumentHandler
    {
        public static void Save(SaveDocumentCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string saveDocumentQuery = "UPDATE Documents " +
                                                 "SET PersonId=@PersonId, " +
                                                 "CorrespondentId=@CorrespondentId, " +
                                                 "Datum=@DatumOntvangen, " +
                                                 "Description=@Beschrijving " +
                                                 "WHERE Id=@Id";

                connection.Execute(saveDocumentQuery, command);
            }
        }
    }
}