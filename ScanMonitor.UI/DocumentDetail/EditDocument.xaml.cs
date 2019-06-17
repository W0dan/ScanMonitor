using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using ScanMonitor.Database.SaveDocument;
using ScanMonitor.UI.Admin.DocumentTypes;
using ScanMonitor.UI.Extensions;
using CheckBox = System.Windows.Controls.CheckBox;
using CustomFieldDto = ScanMonitor.Database.GetDocumentForEdit.CustomFieldDto;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using Label = System.Windows.Controls.Label;
using MessageBox = System.Windows.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

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

            var layoutGrid = new Grid { HorizontalAlignment = HorizontalAlignment.Stretch };
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
                    return CreateTextBox(field);
                case FieldTypes.Numeriek:
                    return CreateNumericBox(field);
                case FieldTypes.Datum:
                    return CreateDatePicker(field);
                case FieldTypes.JaNee:
                    return CreateCheckBox(field);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static CheckBox CreateCheckBox(CustomFieldDto field)
        {
            var checkBox = new CheckBox
            {
                Name = field.FieldName.Namify(),
                VerticalAlignment = VerticalAlignment.Center
            };
            checkBox.SetBinding(ToggleButton.IsCheckedProperty, new Binding("BooleanValue") { Source = field });
            return checkBox;
        }

        private static DatePicker CreateDatePicker(CustomFieldDto field)
        {
            var datePicker = new DatePicker
            {
                Name = field.FieldName.Namify(),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                SelectedDate = field.DateValue
            };
            datePicker.SetBinding(DatePicker.SelectedDateProperty, new Binding("DateValue") { Source = field });
            return datePicker;
        }

        private static TextBox CreateTextBox(CustomFieldDto field)
        {
            var editControl = new TextBox
            {
                Name = field.FieldName.Namify(),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            editControl.SetBinding(TextBox.TextProperty, new Binding("StringValue") { Source = field });

            return editControl;
        }

        private static TextBox CreateNumericBox(CustomFieldDto field)
        {
            var editControl = new TextBox
            {
                Name = field.FieldName.Namify(),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            editControl.SetBinding(TextBox.TextProperty, new Binding("NumericValue") { Source = field });

            return editControl;
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
                PersonId = Model.PersonId,
                CustomFields = Model.CustomFields.Select(x => new Database.SaveDocument.CustomFieldDto
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
