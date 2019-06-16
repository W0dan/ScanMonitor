using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using ScanMonitor.Database.SaveDocument;
using ScanMonitor.UI.Admin.DocumentTypes;
using ScanMonitor.UI.Extensions;
using CustomFieldDto = ScanMonitor.Database.GetDocumentForEdit.CustomFieldDto;

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

            CreateCustomFields(window, model.CustomFields);

            window.ShowDialog();
        }

        private EditDocumentViewModel Model => (EditDocumentViewModel)DataContext;

        private static void CreateCustomFields(EditDocument window, IReadOnlyCollection<CustomFieldDto> customFields)
        {
            var stackPanel = window.CustomFieldsStackPanel;

            var layoutGrid = new Grid {HorizontalAlignment = HorizontalAlignment.Stretch};
            layoutGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(pixels: 200) });
            layoutGrid.ColumnDefinitions.Add(new ColumnDefinition());

            stackPanel.Children.Add(layoutGrid);

            var row = 0;
            foreach (var field in customFields)
            {
                layoutGrid.RowDefinitions.Add(new RowDefinition());

                var label = new Label
                {
                    Content = $"{field.FieldName}:",
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    VerticalContentAlignment = VerticalAlignment.Center,
                };
                label.SetValue(Grid.RowProperty, row);
                label.SetValue(Grid.ColumnProperty, 0);
                layoutGrid.Children.Add(label);

                var editControl = CreateEditControl(field);
                editControl.SetValue(Grid.RowProperty, row);
                editControl.SetValue(Grid.ColumnProperty, 2);
                layoutGrid.Children.Add(editControl);
                
                row++;
            }
        }

        private static UIElement CreateEditControl(CustomFieldDto field)
        {
            switch (field.FieldType.ToEnum<FieldTypes>())
            {
                case FieldTypes.Tekst:
                    return new TextBox
                    {
                        Name = field.FieldName.Namify(),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        Text = field.StringValue ?? ""
                    };
                case FieldTypes.Numeriek:
                    return new TextBox
                    {
                        Name = field.FieldName.Namify(),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        Text = field.NumericValue?.ToString("0") ?? "0"
                    };
                case FieldTypes.Datum:
                    return new DatePicker
                    {
                        Name = field.FieldName.Namify(),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        SelectedDate = field.DateValue
                    };
                case FieldTypes.JaNee:
                    return new CheckBox
                    {
                        Name = field.FieldName.Namify(),
                        VerticalAlignment = VerticalAlignment.Center,
                        IsChecked = field.BooleanValue
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

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
                PersonId = Model.PersonId
            };
            SaveDocumentHandler.Save(command);

            MessageBox.Show("Wijzigingen bewaard", "Wijzigingen bewaard", MessageBoxButton.OK);
        }
    }
}
