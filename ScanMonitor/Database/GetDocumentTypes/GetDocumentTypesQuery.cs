using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace ScanMonitor.Database.GetDocumentTypes
{
    public static class GetDocumentTypesQuery
    {
        public static List<DocumentTypeDto> List()
        {
            using (var dbConnection = DatabaseHelper.GetConnection())
                return dbConnection.Query<DocumentTypeDto>("SELECT Id, Name FROM DocumentTypes ORDER BY Name").ToList();
        }
    }
}