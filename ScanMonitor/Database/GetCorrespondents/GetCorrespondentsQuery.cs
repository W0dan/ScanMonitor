using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace ScanMonitor.Database.GetCorrespondents
{
    public static class GetCorrespondentsQuery
    {
        public static List<CorrespondentDto> List()
        {
            using (var dbConnection = DatabaseHelper.GetConnection())
                return dbConnection.Query<CorrespondentDto>("SELECT Id, Name FROM Correspondents").ToList();
        }
    }
}