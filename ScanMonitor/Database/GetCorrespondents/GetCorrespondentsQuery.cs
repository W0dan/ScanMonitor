using System.Collections.Generic;
using System.Linq;
using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.GetCorrespondents
{
    public static class GetCorrespondentsQuery
    {
        public static List<CorrespondentDto> List()
        {
            using (var dbConnection = AppConfig.Connections.ScanDbMySql)
                return dbConnection.Query<CorrespondentDto>("SELECT Id, Name FROM Correspondents ORDER BY Name").ToList();
        }
    }
}