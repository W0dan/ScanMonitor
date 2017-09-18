using System;
using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.DocumentTypeToevoegen
{
    public class DocumentTypeToevoegenQuery
    {
        public static void Insert(DocumentTypeToevoegenCommand command)
        {
            using (var connection = AppConfig.Connections.ScanDbMySql)
            {
                const string query = "INSERT INTO DocumentTypes(Id, Name) " +
                                     "VALUES(@Id, @Name)";

                connection.Execute(query, command);
            }
        }
    }
}