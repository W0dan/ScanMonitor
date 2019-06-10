using System;
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
            string query;

            switch (AppConfig.Connections.ConnectionCompatibility)
            {
                case ConnectionCompatibility.SqlServer:
                    query = "select distinct Id, Name\r\n" +
                            "From(\r\n" +
                            "   select top 100 percent d.id, convert(nvarchar(max), d.datum, 102)+dt.Name+' van '+c.Name+' voor '+p.name as Name, s.datum\r\n" +
                            "   from documents d\r\n" +
                            "   inner join scans s on s.DocumentId = d.Id\r\n" +
                            "   inner join documenttypes dt on dt.Id = d.DocumentTypeId\r\n" +
                            "   inner join correspondents c on d.CorrespondentId = c.Id\r\n" +
                            "   inner join People p on d.PersonId = p.Id\r\n" +
                            $"   where s.Datum > '{DateTime.Now:yyyy-MM-dd}'\r\n" +
                            "   order by s.Datum\r\n" +
                            ") as tmp";
                    break;
                case ConnectionCompatibility.MySql:
                    query = "SELECT DISTINCT Id, Name\r\n" +
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
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            using (var dbConnection = AppConfig.Connections.ScanDbConnection)
            {
                return dbConnection.Query<DocumentDto>(query).ToList();
            }
        }
    }
}