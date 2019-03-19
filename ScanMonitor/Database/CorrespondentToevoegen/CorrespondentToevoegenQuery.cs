using System;
using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.CorrespondentToevoegen
{
    public class CorrespondentToevoegenQuery
    {
        public static void Insert(CorrespondentToevoegenCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                const string query = "INSERT INTO Correspondents(Id, Name) " +
                                     "VALUES(@Id, @Name)";

                connection.Execute(query, command);
            }
        }
    }
}