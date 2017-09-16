using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace ScanMonitor.Database.GetPeople
{
    public static class GetPeopleQuery
    {
        public static List<PersonDto> List()
        {
            using (var dbConnection = DatabaseHelper.GetConnection())
                return dbConnection.Query<PersonDto>("SELECT Id, Name FROM People").ToList();
        }
    }
}