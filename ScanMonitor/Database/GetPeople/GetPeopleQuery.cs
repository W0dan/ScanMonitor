using System.Collections.Generic;
using System.Linq;
using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.GetPeople
{
    public static class GetPeopleQuery
    {
        public static List<PersonDto> List()
        {
            using (var dbConnection = AppConfig.Connections.ScanDbMySql)
                return dbConnection.Query<PersonDto>("SELECT Id, Name FROM People").ToList();
        }
    }
}