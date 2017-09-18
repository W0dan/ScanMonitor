using System;
using System.IO;
using ScanMonitor.Config;
using ScanMonitor.Database.CreateDocument;
using ScanMonitor.Database.CreateScan;
using ScanMonitor.Database.GetDocumentFolderInfo;

namespace ScanMonitor.Logic.NewScan
{
    public static class NewScanHandler
    {
        public static void Handle(NewScanCommand command)
        {
            var documentId = command.DocumentId.HasValue 
                ? command.DocumentId?.ToString() 
                : InsertDocument(command);

            var scannedDocumentId = Guid.NewGuid().ToString();
            var destinationFilename = MoveDocumentToCorrectLocation(command.Filename, documentId, scannedDocumentId);

            InsertScannedFile(destinationFilename, documentId, scannedDocumentId);
        }

        private static string MoveDocumentToCorrectLocation(string filename, string documentId, string scannedDocumentId)
        {
            var documentInfo = GetDocumentFolderInfoQuery.Get(documentId);

            var destinationFolder = Path.Combine(AppConfig.AppSettings.RootDocumentPath, documentInfo.Person, documentInfo.Correspondent);
            var folder = new DirectoryInfo(destinationFolder);
            if (!folder.Exists)
            {
                folder.Create();
            }

            var documentType = documentInfo.DocumentType.Replace(" ", "_");

            var scannedFile = new FileInfo(filename);
            var fileName = $"{documentInfo.Datum:yyyy-MM-dd}_{documentType}_{scannedDocumentId}{scannedFile.Extension}";
            var destinationFilename = Path.Combine(folder.FullName, fileName);

            scannedFile.MoveTo(destinationFilename);
            return destinationFilename;
        }

        private static void InsertScannedFile(string destinationFilename, string documentId, string scannedDocumentId)
        {
            CreateScanQuery.Insert(new CreateScanCommand
            {
                Id = scannedDocumentId,
                Datum = DateTime.Now,
                Filename = destinationFilename,
                DocumentId = documentId
            });
        }

        private static string InsertDocument(NewScanCommand command)
        {
            var documentId = Guid.NewGuid().ToString();
            CreateDocumentQuery.Insert(new CreateDocumentCommand
            {
                Id = documentId,
                PersonId = command.PersonId,
                CorrespondentId = command.CorrespondentId.ToString(),
                DocumentTypeId = command.DocumentTypeId.ToString(),
                Datum = command.Datum,
                Description = command.Description
            });
            return documentId;
        }
    }
}