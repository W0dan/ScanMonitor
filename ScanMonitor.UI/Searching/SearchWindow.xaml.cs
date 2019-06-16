using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using ScanMonitor.Database.DeleteDocument;
using ScanMonitor.Database.GetCorrespondents;
using ScanMonitor.Database.GetDocumentForEdit;
using ScanMonitor.Database.GetDocumentTypes;
using ScanMonitor.Database.GetPeople;
using ScanMonitor.Database.SearchDocuments;
using ScanMonitor.Exceptions;
using ScanMonitor.UI.DocumentDetail;
using DocumentDto = ScanMonitor.Database.SearchDocuments.DocumentDto;

namespace ScanMonitor.UI.Searching
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
        }

        public static void StartSearch(Window owner)
        {
            var window = new SearchWindow
            {
                Title = "Documenten zoeken",
                Owner = owner
            };

            window.ShowDialog();
        }

        private void SearchWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadCorrespondents();
            LoadPeople();
            LoadDocumentTypes();
        }

        private void LoadCorrespondents()
        {
            var correspondentDtos = GetCorrespondentsQuery.List();
            correspondentDtos.Insert(0, new CorrespondentDto());
            CorrespondentDropdown.ItemsSource = correspondentDtos;
            CorrespondentDropdown.DisplayMemberPath = nameof(DocumentTypeDto.Name);
            CorrespondentDropdown.SelectedValuePath = nameof(DocumentTypeDto.Id);
        }

        private void LoadPeople()
        {
            var people = GetPeopleQuery.List();
            people.Insert(0, new PersonDto());
            VoorWieDropdown.ItemsSource = people;
            VoorWieDropdown.DisplayMemberPath = nameof(DocumentTypeDto.Name);
            VoorWieDropdown.SelectedValuePath = nameof(DocumentTypeDto.Id);
        }

        private void LoadDocumentTypes()
        {
            var documentTypeDtos = GetDocumentTypesQuery.List();
            documentTypeDtos.Insert(0, new DocumentTypeDto());
            DocumentTypeDropdown.ItemsSource = documentTypeDtos;
            DocumentTypeDropdown.DisplayMemberPath = nameof(DocumentTypeDto.Name);
            DocumentTypeDropdown.SelectedValuePath = nameof(DocumentTypeDto.Id);
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            var request = new SearchDocumentsRequest
            {
                CorrespondentId = (string)CorrespondentDropdown.SelectedValue,
                DocumentTypeId = (string)DocumentTypeDropdown.SelectedValue,
                PersonId = (string)VoorWieDropdown.SelectedValue,
                Datum = DatumOntvangenDatePicker.SelectedDate,
                SearchString = DescriptionTextbox.Text
            };
            var results = SearchDocumentsQuery.List(request);

            FoundItemsDataGrid.DataContext = new ObservableCollection<DocumentDto>(results);
        }

        private void OnHyperlinkClicked(object sender, RoutedEventArgs e)
        {
            var destination = ((Hyperlink)e.OriginalSource).NavigateUri;
            Process.Start(destination.ToString());
        }

        private void OnDeleteClicked(object sender, MouseButtonEventArgs e)
        {
            var document = (DocumentDto)((FrameworkElement)sender).DataContext;

            var result = MessageBox.Show($"Ben je zeker dat je document {document.Beschrijving} van " +
                            $"{document.Correspondent} voor {document.VoorWie} op datum " +
                            $"{document.DatumOntvangen:dd-MM-yyy} en álle daaraan gerelateerde scans " +
                            $"wil verwijderen ?",
                            "Document verwijderen", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No) return;

            var warnings = DeleteDocumentHandler.Handle(new DeleteDocumentCommand
            {
                Id = document.Id
            });

            var dataContext = (ObservableCollection<DocumentDto>)FoundItemsDataGrid.DataContext;
            var doc = dataContext.Single(x => x.Id == document.Id);
            dataContext.Remove(doc);

            if (!warnings.Any()) return;

            var message = warnings
                .Aggregate("Sommige bestanden konden niet verwijderd worden:\n",
                (current, warning) => $"{current}-{warning}\n");

            throw new ScanMonitorException(message);
        }

        private void OnDetailClicked(object sender, MouseButtonEventArgs e)
        {
            // -> new window : edit details of admin item  (specific)
            var clickedItem = (DocumentDto)((FrameworkElement)sender).DataContext;

            var documentForEdit = GetDocumentForEditQuery.Get(new GetDocumentForEditRequest { Id = clickedItem.Id });

            // -> navigate to detail of document
            var editDocumentViewModel = new EditDocumentViewModel
            {
                Id = documentForEdit.Id,
                DocumentType = documentForEdit.DocumentType,
                Beschrijving = documentForEdit.Beschrijving,
                CorrespondentId = documentForEdit.CorrespondentId,
                Datum = documentForEdit.DatumOntvangen,
                PersonId = documentForEdit.PersonId,
                CustomFields = new ObservableCollection<CustomFieldDto>(documentForEdit.CustomFields),
                Scans = new ObservableCollection<ScanDto>(documentForEdit.Scans)
            };

            EditDocument.ShowEditDocument(this, editDocumentViewModel);
        }
    }
}
