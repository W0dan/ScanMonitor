using System;
using Dapper;

namespace ScanMonitor.Database.DocumentTypeToevoegen
{
    public class DocumentTypeToevoegenQuery
    {
        public static void Insert(DocumentTypeToevoegenCommand command)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                const string query = "INSERT INTO DocumentTypes(Id, Name) " +
                                     "VALUES(@Id, @Name)";

                connection.Execute(query, command);
            }
        }
    }
}