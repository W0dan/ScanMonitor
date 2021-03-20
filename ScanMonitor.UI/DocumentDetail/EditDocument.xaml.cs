using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using ScanMonitor.Database.SaveDocument;
using ScanMonitor.UI.Extensions;

namespace ScanMonitor.UI.DocumentDetail
{
    /// <summary>
    /// Interaction logic for EditDocument.xaml
    /// </summary>
    public partial class EditDocument : Window
    {
        public EditDocument()
        {
            InitializeComponent();
        }

        public static void ShowEditDocument(Window owner, EditDocumentViewModel model)
        {
            var window = new EditDocument
            {
                Title = "Document bewerken",
                Owner = owner,
                DataContext = model
            };

            window.CustomFieldsStackPanel.CreateCustomFields(model.CustomFields);

            window.ShowDialog();
        }

        private EditDocumentViewModel Model => (EditDocumentViewModel)DataContext;

        private void OnHyperlinkClicked(object sender, RoutedEventArgs e)
        {
            var destination = ((Hyperlink)e.OriginalSource).NavigateUri;
            Process.Start(destination.ToString());
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            // todo: map viewmodel to SaveDocumentCommand !!
            // !! validation !!

            var command = new SaveDocumentCommand
            {
                Id = Model.Id,
                CorrespondentId = Model.CorrespondentId,
                Beschrijving = Model.Beschrijving,
                DatumOntvangen = Model.Datum,
                PersonId = Model.PersonId,
                CustomFields = Model.CustomFields.Select(x => new CustomFieldDto
                {
                    Id = x.Id,
                    DocumentTypeCustomFieldId = x.DocumentTypeCustomFieldId,
                    FieldType = x.FieldType,
                    FieldName = x.FieldName,
                    NumericValue = x.NumericValue,
                    StringValue = x.StringValue,
                    BooleanValue = x.BooleanValue,
                    DateValue = x.DateValue
                }).ToList()
            };
            SaveDocumentHandler.Save(command);

            MessageBox.Show("Wijzigingen bewaard", "Wijzigingen bewaard", MessageBoxButton.OK);
        }
    }
}
