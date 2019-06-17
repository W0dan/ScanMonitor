using System;
using Dapper;
using ScanMonitor.Config;

namespace ScanMonitor.Database.SaveDocument
{
    public class SaveDocumentHandler
    {
        public static void Save(SaveDocumentCommand command)
        {
            const string saveDocumentQuery = "UPDATE Documents " +
                                             "SET PersonId=@PersonId, " +
                                             "CorrespondentId=@CorrespondentId, " +
                                             "Datum=@DatumOntvangen, " +
                                             "Description=@Beschrijving " +
                                             "WHERE Id=@Id";

            const string saveNewCustomFieldQuery = "INSERT INTO DocumentCustomFields(Id, DocumentId, DocumentTypeCustomFieldId, StringValue, DecimalValue, BooleanValue, DateValue) " +
                                                   "VALUES (@Id, @DocumentId, @DocumentTypeCustomFieldId, @StringValue, @NumericValue, @BooleanValue, @DateValue)";
            const string saveExistingCustomFieldQuery = "UPDATE DocumentCustomFields " +
                                                        "SET StringValue=@StringValue," +
                                                        "DecimalValue=@NumericValue," +
                                                        "BooleanValue=@BooleanValue," +
                                                        "DateValue=@DateValue " +
                                                        "WHERE Id=@Id";

            using (var connection = AppConfig.Connections.ScanDbConnection)
            {
                foreach (var field in command.CustomFields)
                {
                    if (string.IsNullOrWhiteSpace(field.Id))
                        connection.Execute(saveNewCustomFieldQuery, new
                        {
                            Id = Guid.NewGuid().ToString(),
                            DocumentId = command.Id,
                            field.DocumentTypeCustomFieldId,
                            field.StringValue,
                            field.NumericValue,
                            field.BooleanValue,
                            field.DateValue
                        });
                    else
                        connection.Execute(saveExistingCustomFieldQuery, field);
                }

                connection.Execute(saveDocumentQuery, command);
            }
        }
    }
}