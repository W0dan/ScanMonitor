using System;
using System.Collections.Generic;
using System.IO;
using ScanMonitor.Database.GetDocumentById;

namespace ScanMonitor.Database.DeleteDocument
{
    public class DeleteDocumentHandler
    {
        public static List<string> Handle(DeleteDocumentCommand command)
        {
            var document = GetDocumentByIdQuery.Get(new GetDocumentByIdRequest { Id = command.Id });
            var warnings = new List<string>();

            foreach (var scan in document.Scans)
            {
                var fi = new FileInfo(scan.Filename);
                try
                {
                    if (fi.Exists)
                        fi.Delete();
                }
                catch (Exception e)
                {
                    warnings.Add($"{e.Message}");
                }
            }

            DeleteDocumentQuery.Delete(command);

            return warnings;
        }
    }
}