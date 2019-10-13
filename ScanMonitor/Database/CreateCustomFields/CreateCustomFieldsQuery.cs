using Dapper;
using ScanMonitor.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanMonitor.Database.CreateCustomFields
{
    public class CreateCustomFieldsQuery
    {
        public static void Insert(CreateCustomFieldsCommand command)
        {
            const string saveNewCustomFieldQuery = "INSERT INTO DocumentCustomFields(Id, DocumentId, DocumentTypeCustomFieldId, StringValue, DecimalValue, BooleanValue, DateValue) " +
                                       "VALUES (@Id, @DocumentId, @DocumentTypeCustomFieldId, @StringValue, @NumericValue, @BooleanValue, @DateValue)";

            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                foreach (var field in command.CustomFields)
                {
                        connection.Execute(saveNewCustomFieldQuery, new
                        {
                            Id = Guid.NewGuid().ToString(),
                            command.DocumentId,
                            field.DocumentTypeCustomFieldId,
                            field.StringValue,
                            field.NumericValue,
                            field.BooleanValue,
                            field.DateValue
                        });
                }
            }
        }
    }
}
