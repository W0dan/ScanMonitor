namespace ScanMonitor.UI.Searching
{
    public class DocumentViewModel : Database.SearchDocuments.DocumentDto
    {
        public DocumentViewModel(Database.SearchDocuments.DocumentDto dto)
        {
            this.Id = dto.Id;
            this.DocumentType = dto.DocumentType;
            this.VoorWie = dto.VoorWie;
            this.Correspondent = dto.Correspondent;
            this.DatumOntvangen = dto.DatumOntvangen;
            this.Beschrijving = dto.Beschrijving;
            this.Files = dto.Files;
        }

        public System.Windows.Visibility FileVisibility => FileList.Count > 0 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
    }
}
