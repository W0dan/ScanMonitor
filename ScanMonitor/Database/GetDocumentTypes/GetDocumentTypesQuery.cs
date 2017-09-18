using System.Collections.Generic;
using System.Linq;
using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.GetDocumentTypes
{
    public static class GetDocumentTypesQuery
    {
        public static List<DocumentTypeDto> List()
        {
            using (var dbConnection = AppConfig.Connections.ScanDbMySql)
                return dbConnection.Query<DocumentTypeDto>("SELECT Id, Name FROM DocumentTypes ORDER BY Name").ToList();
        }
    }
}