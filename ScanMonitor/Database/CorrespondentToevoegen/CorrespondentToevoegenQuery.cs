using System;
using Dapper;

namespace ScanMonitor.Database.CorrespondentToevoegen
{
    public class CorrespondentToevoegenQuery
    {
        public static void Insert(CorrespondentToevoegenCommand command)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                const string query = "INSERT INTO Correspondents(Id, Name) " +
                                     "VALUES(@Id, @Name)";

                connection.Execute(query, command);
            }
        }
    }
}