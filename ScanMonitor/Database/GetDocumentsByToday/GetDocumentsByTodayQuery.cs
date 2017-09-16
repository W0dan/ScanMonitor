using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace ScanMonitor.Database.GetDocumentsByToday
{
    public static class GetDocumentsByTodayQuery
    {
        public static List<DocumentDto> List()
        {
            const string query = "SELECT distinct d.Id, (dt.Name + ' van ') as Name " +
                                 "FROM documents d, Scans s, DocumentTypes dt, Correspondents c, People p " +
                                 "WHERE d.DocumentTypeId = dt.Id " +
                                 "AND d.PersonId = p.Id " +
                                 "AND d.CorrespondentId = c.Id " +
                                 "AND s.Datum > curdate()";

            using (var dbConnection = DatabaseHelper.GetConnection())
            {
                return dbConnection.Query<DocumentDto>(query).ToList();
            }
        }
    }
}