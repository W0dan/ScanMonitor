using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ScanMonitor.Database.CorrespondentToevoegen;
using ScanMonitor.Database.DocumentTypeToevoegen;
using ScanMonitor.Database.GetCorrespondents;
using ScanMonitor.Database.GetDocumentsByToday;
using ScanMonitor.Database.GetDocumentTypes;
using ScanMonitor.Database.GetPeople;
using ScanMonitor.Logic.ProcessDocument;
using ScanMonitor.UI.Dialogs;
using ScanMonitor.UI.Extensions;
using static ScanMonitor.UI.Extensions.TryExtensions;

namespace ScanMonitor.UI
{
    public partial class IndexWindow : Window
    {
        private ObservableCollection<CorrespondentDto> _correspondentDropdownItemsSource;
        private ObservableCollection<DocumentTypeDto> _documentTypeDropdownItemsSource;
        private string FileName { get; set; }

        public IndexWindow()
        {
            InitializeComponent();
        }

        public static void ProcessFile(Window owner, string fileName)
        {
            var window = new IndexWindow
            {
                FileName = fileName,
                Owner = owner
            };

            window.ShowDialog();
        }

        private void IndexWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            InitialiseFields();

            var canAccessFile = false;
            Try(() => canAccessFile = FileName.CanAccessFile()).Times(10);
            if (!canAccessFile) return;

            ShowFile();
        }

        private void InitialiseFields()
        {
            LoadDocumentTypes();
            LoadPeople();
            LoadCorrespondents();
            LoadDocuments();
            DatumOntvangenDatePicker.SelectedDate = DateTime.Today;
        }

        private void LoadDocuments()
        {
            var documents = GetDocumentsByTodayQuery.List();
            DocumentsDropdown.ItemsSource = documents;
            DocumentsDropdown.DisplayMemberPath = nameof(DocumentTypeDto.Name);
            DocumentsDropdown.SelectedValuePath = nameof(DocumentTypeDto.Id);
        }

        private void LoadCorrespondents()
        {
            _correspondentDropdownItemsSource = new ObservableCollection<CorrespondentDto>(GetCorrespondentsQuery.List());
            CorrespondentDropdown.ItemsSource = _correspondentDropdownItemsSource;
            CorrespondentDropdown.DisplayMemberPath = nameof(DocumentTypeDto.Name);
            CorrespondentDropdown.SelectedValuePath = nameof(DocumentTypeDto.Id);
        }

        private void LoadPeople()
        {
            var people = GetPeopleQuery.List();
            VoorWieDropdown.ItemsSource = people;
            VoorWieDropdown.DisplayMemberPath = nameof(DocumentTypeDto.Name);
            VoorWieDropdown.SelectedValuePath = nameof(DocumentTypeDto.Id);
        }

        private void LoadDocumentTypes()
        {
            _documentTypeDropdownItemsSource = new ObservableCollection<DocumentTypeDto>(GetDocumentTypesQuery.List());
            DocumentTypeDropdown.ItemsSource = _documentTypeDropdownItemsSource;
            DocumentTypeDropdown.DisplayMemberPath = nameof(DocumentTypeDto.Name);
            DocumentTypeDropdown.SelectedValuePath = nameof(DocumentTypeDto.Id);
        }

        private void ShowFile()
        {
            var fileExtension = FileName.FileExtension();
            switch (fileExtension)
            {
                case ".pdf":
                    ShowPdfFile();
                    break;
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".bmp":
                    ShowImageFile();
                    break;
                default:
                    ShowCannotShow(fileExtension);
                    break;
            }
        }

        private void ShowCannotShow(string fileExtension)
        {
            var label = new Label { Content = $"Kan op dit ogenblik nog geen bestanden tonen met extensie {fileExtension}" };

            LayoutGrid.Children.Add(label);
        }

        private void ShowImageFile()
        {
            var image = new Image { Source = new BitmapImage(new Uri(FileName)) };

            LayoutGrid.Children.Add(image);
        }

        private void ShowPdfFile()
        {
            var host = new WindowsFormsHost();

            var pdfViewer = new Spire.PdfViewer.Forms.PdfViewer();

            pdfViewer.LoadFromFile(FileName);
            host.Child = pdfViewer;

            LayoutGrid.Children.Add(host);
        }

        private void NewDocumentRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            NewDocumentGrid.Visibility = Visibility.Visible;
            ExistingDocumentGrid.Visibility = Visibility.Hidden;
        }

        private void ExistingDocumentRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            NewDocumentGrid.Visibility = Visibility.Hidden;
            ExistingDocumentGrid.Visibility = Visibility.Visible;
        }

        private void VerwijderenButton_OnClick(object sender, RoutedEventArgs e)
        {
            // remove document from scans folder

            Close();
        }

        private void ToevoegenButton_OnClick(object sender, RoutedEventArgs e)
        {
            var validationErrors = "";

            DocumentTypeLabel.Background = Brushes.Transparent;
            VoorWieLabel.Background = Brushes.Transparent;
            CorrespondentLabel.Background = Brushes.Transparent;

            // process document
            var documentTypeIdString = DocumentTypeDropdown.SelectedValue?.ToString() ?? "";
            if (string.IsNullOrEmpty(documentTypeIdString))
            {
                validationErrors += "Document type is verplicht\n";
                DocumentTypeLabel.Background = Brushes.Red;
            }

            var personIdString = VoorWieDropdown.SelectedValue?.ToString() ?? "";
            if (string.IsNullOrEmpty(personIdString))
            {
                validationErrors += "'Voor wie' is verplicht\n";
                VoorWieLabel.Background = Brushes.Red;
            }

            var correspondentIdString = CorrespondentDropdown.SelectedValue?.ToString() ?? "";
            if (string.IsNullOrEmpty(correspondentIdString))
            {
                validationErrors += "Correspondent is verplicht\n";
                CorrespondentLabel.Background = Brushes.Red;
            }

            if (!string.IsNullOrEmpty(validationErrors))
            {
                ValidationErrorsLabel.Content = validationErrors;
                return;
            }

            var command = new NewDocumentCommand
            {
                Filename = FileName,
                DocumentTypeId = new Guid(documentTypeIdString),
                PersonId = int.Parse(personIdString),
                CorrespondentId = new Guid(correspondentIdString),
                Datum = DatumOntvangenDatePicker.SelectedDate ?? DateTime.Today,
                Description = DescriptionTextbox.Text
            };
            NewDocumentHandler.Handle(command);

            Close();
        }

        private void AddDocumentTypeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var newDocumentType = InputBox.Show(this, "Documenttype toevoegen", "Documenttype:");
            if (newDocumentType == null) return;

            var command = new DocumentTypeToevoegenCommand { Id = Guid.NewGuid().ToString(), Name = newDocumentType };
            DocumentTypeToevoegenQuery.Insert(command);

            _documentTypeDropdownItemsSource.Add(new DocumentTypeDto { Id = command.Id, Name = command.Name });

            DocumentTypeDropdown.SelectedValue = command.Id;
        }

        private void AddCorrespondentButton_OnClick(object sender, RoutedEventArgs e)
        {
            var newCorrespondent = InputBox.Show(this, "Correspondent toevoegen", "Correspondent:");
            if (newCorrespondent == null) return;

            var command = new CorrespondentToevoegenCommand { Id = Guid.NewGuid().ToString(), Name = newCorrespondent };
            CorrespondentToevoegenQuery.Insert(command);

            _correspondentDropdownItemsSource.Add(new CorrespondentDto { Id = command.Id, Name = command.Name });

            CorrespondentDropdown.SelectedValue = command.Id;
        }
    }
}
