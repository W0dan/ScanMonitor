using System.Collections.Generic;
using System.Linq;
using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.GetDocumentsByToday
{
    public static class GetDocumentsByTodayQuery
    {
        public static List<DocumentDto> List()
        {
            const string query = "SELECT DISTINCT Id, Name\r\n" +
                                 "FROM (\r\n" +
                                 "    SELECT d.Id, CONCAT(DATE_FORMAT(d.Datum, \'%Y-%m-%e\'), \'  \', dt.Name, \' van \', c.Name, \' voor \', p.Name) as Name, s.Datum\r\n" +
                                 "    FROM documents d, Scans s, DocumentTypes dt, Correspondents c, People p \r\n" +
                                 "    WHERE d.DocumentTypeId = dt.Id \r\n" +
                                 "    AND d.PersonId = p.Id \r\n" +
                                 "    AND d.CorrespondentId = c.Id \r\n" +
                                 "    AND s.DocumentId = d.Id\r\n" +
                                 "    AND s.Datum > curdate()\r\n" +
                                 "    order by s.Datum DESC\r\n" +
                                 ") as tmp";

            using (var dbConnection = AppConfig.Connections.ScanDbMySql)
            {
                return dbConnection.Query<DocumentDto>(query).ToList();
            }
        }
    }
}