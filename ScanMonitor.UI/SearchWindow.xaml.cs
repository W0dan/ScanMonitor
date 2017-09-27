using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using ScanMonitor.Database.GetCorrespondents;
using ScanMonitor.Database.GetDocumentTypes;
using ScanMonitor.Database.GetPeople;
using ScanMonitor.Database.SearchDocuments;

namespace ScanMonitor.UI
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
                PersonId = (int?)VoorWieDropdown.SelectedValue,
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
    }
}
