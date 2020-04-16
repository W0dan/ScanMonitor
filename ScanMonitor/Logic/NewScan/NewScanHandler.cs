using System;
using System.Collections.Generic;
using System.IO;
using ScanMonitor.Config;
using ScanMonitor.Database.CreateCustomFields;
using ScanMonitor.Database.CreateDocument;
using ScanMonitor.Database.CreateScan;
using ScanMonitor.Database.GetCustomFields;
using ScanMonitor.Database.GetDocumentFolderInfo;

namespace ScanMonitor.Logic.NewScan
{
    public static class NewScanHandler
    {
        public static void Handle(NewScanCommand command)
        {
            var isNewDocument = !command.DocumentId.HasValue;
            var documentId = isNewDocument 
                ? InsertDocument(command)
                : command.DocumentId?.ToString();

            var scannedDocumentId = Guid.NewGuid().ToString();
            var destinationFilename = MoveDocumentToCorrectLocation(command.Filename, documentId, scannedDocumentId);

            InsertScannedFile(destinationFilename, documentId, scannedDocumentId);
            if (isNewDocument)
                InsertCustomFields(documentId, command.CustomFields);
        }

        private static void InsertCustomFields(string documentId, List<CustomFieldDto> customFields)
        {
            CreateCustomFieldsQuery.Insert(new CreateCustomFieldsCommand
            {
                DocumentId = documentId,
                CustomFields = customFields
            });
        }

        private static string MoveDocumentToCorrectLocation(string filename, string documentId, string scannedDocumentId)
        {
            var documentInfo = GetDocumentFolderInfoQuery.Get(documentId);

            var partialFolder = Path.Combine(documentInfo.Person, documentInfo.Correspondent);
            var destinationFolder = Path.Combine(AppConfig.AppSettings.RootDocumentPath, partialFolder);
            var folder = new DirectoryInfo(destinationFolder);
            if (!folder.Exists)
            {
                folder.Create();
            }

            var documentType = documentInfo.DocumentType.Replace(" ", "_");

            var scannedFile = new FileInfo(filename);
            var fileName = $"{documentInfo.Datum:yyyy-MM-dd}_{documentType}_{scannedDocumentId}{scannedFile.Extension}";

            scannedFile.MoveTo(Path.Combine(folder.FullName, fileName));
            return Path.Combine(partialFolder, fileName);
        }

        private static void InsertScannedFile(string destinationFilename, string documentId, string scannedDocumentId)
        {
            CreateScanQuery.Insert(new CreateScanCommand
            {
                Id = scannedDocumentId,
                Datum = DateTime.Now,
                PartialFilename = destinationFilename,
                DocumentId = documentId
            });
        }

        private static string InsertDocument(NewScanCommand command)
        {
            var documentId = Guid.NewGuid().ToString();
            CreateDocumentQuery.Insert(new CreateDocumentCommand
            {
                Id = documentId,
                PersonId = command.PersonId.ToString(),
                CorrespondentId = command.CorrespondentId.ToString(),
                DocumentTypeId = command.DocumentTypeId.ToString(),
                Datum = command.Datum,
                Description = command.Description
            });
            return documentId;
        }
    }
}