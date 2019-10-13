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
using ScanMonitor.Database.GetCustomFields;
using ScanMonitor.Database.GetDocumentsByToday;
using ScanMonitor.Database.GetDocumentTypes;
using ScanMonitor.Database.GetPeople;
using ScanMonitor.Logic.NewScan;
using ScanMonitor.Logic.RemoveScan;
using ScanMonitor.UI.Dialogs;
using ScanMonitor.UI.Extensions;

namespace ScanMonitor.UI.Indexing
{
    public partial class IndexWindow : Window
    {
        private ObservableCollection<CorrespondentDto> correspondentDropdownItemsSource;
        private ObservableCollection<DocumentTypeDto> documentTypeDropdownItemsSource;
        private List<CustomFieldDto> customFields;
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
            TryExtensions.Try(() => canAccessFile = FileName.CanAccessFile()).Times(10);
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
            correspondentDropdownItemsSource = new ObservableCollection<CorrespondentDto>(GetCorrespondentsQuery.List());
            CorrespondentDropdown.ItemsSource = correspondentDropdownItemsSource;
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
            documentTypeDropdownItemsSource = new ObservableCollection<DocumentTypeDto>(GetDocumentTypesQuery.List());
            DocumentTypeDropdown.ItemsSource = documentTypeDropdownItemsSource;
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
            var scannedImage = new BitmapImage();
            scannedImage.BeginInit();
            scannedImage.CacheOption = BitmapCacheOption.OnLoad;
            scannedImage.UriSource = new Uri(FileName);
            scannedImage.EndInit();

            LayoutGrid.Children.Add(new Image { Source = scannedImage });
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
            RemoveScanHandler.Handle(new RemoveScanCommand { Filename = FileName });

            Close();
        }

        private void ToevoegenButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ExistingDocumentRadioButton.IsChecked ?? false)
            {
                ScanToevoegenAanBestaandDocument();
            }
            else
            {
                NieuwDocument();
            }
        }

        private void ScanToevoegenAanBestaandDocument()
        {
            var documentIdString = DocumentsDropdown.SelectedValue?.ToString() ?? "";
            if (string.IsNullOrEmpty(documentIdString))
            {
                DocumentsLabel.Background = Brushes.Red;
                ValidationErrorsLabel.Content = "Gelieve een document te selecteren";
            }

            var command = new NewScanCommand
            {
                Filename = FileName,
                DocumentId = new Guid(documentIdString),
            };
            NewScanHandler.Handle(command);

            Close();
        }

        private void NieuwDocument()
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

            var command = new NewScanCommand
            {
                Filename = FileName,
                DocumentTypeId = new Guid(documentTypeIdString),
                PersonId = personIdString,
                CorrespondentId = new Guid(correspondentIdString),
                Datum = DatumOntvangenDatePicker.SelectedDate ?? DateTime.Today,
                Description = DescriptionTextbox.Text,
                CustomFields = customFields
            };
            NewScanHandler.Handle(command);

            Close();
        }

        private void AddDocumentTypeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var newDocumentType = InputBox.Show(this, "Documenttype toevoegen", "Documenttype:");
            if (newDocumentType == null) return;

            var command = new DocumentTypeToevoegenCommand { Id = Guid.NewGuid().ToString(), Name = newDocumentType };
            DocumentTypeToevoegenQuery.Insert(command);

            documentTypeDropdownItemsSource.Add(new DocumentTypeDto { Id = command.Id, Name = command.Name });

            DocumentTypeDropdown.SelectedValue = command.Id;
        }

        private void AddCorrespondentButton_OnClick(object sender, RoutedEventArgs e)
        {
            var newCorrespondent = InputBox.Show(this, "Correspondent toevoegen", "Correspondent:");
            if (newCorrespondent == null) return;

            var command = new CorrespondentToevoegenCommand { Id = Guid.NewGuid().ToString(), Name = newCorrespondent };
            CorrespondentToevoegenQuery.Insert(command);

            correspondentDropdownItemsSource.Add(new CorrespondentDto { Id = command.Id, Name = command.Name });

            CorrespondentDropdown.SelectedValue = command.Id;
        }

        private void OverslaanButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DocumentTypeDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var documentTypeId = ((DocumentTypeDto)DocumentTypeDropdown.SelectedItem).Id;

            customFields = GetCustomFieldsQuery.Get(new GetCustomFieldsRequest { DocumentTypeId = documentTypeId });

            CustomFieldsStackPanel.CreateCustomFields(customFields);
        }
    }
}
