using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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

        private static void CreateCustomFields(EditDocument window, IEnumerable<CustomFieldDto> customFields)
        {
            var stackPanel = window.CustomFieldsStackPanel;

            foreach (var field in customFields)
            {
                var customFieldContainer = new StackPanel { Orientation = Orientation.Horizontal };
                customFieldContainer.Children.Add(new Label
                {
                    Width = 200,
                    Content = field.FieldName,
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    VerticalContentAlignment = VerticalAlignment.Center
                });
                var fieldType = field.FieldType.ToEnum<FieldTypes>();
                switch (fieldType)
                {
                    case FieldTypes.Tekst:
                        customFieldContainer.Children.Add(new TextBox { Name = field.FieldName.Namify(), Text = field.StringValue ?? "" });
                        break;
                    case FieldTypes.Numeriek:
                        customFieldContainer.Children.Add(new TextBox { Name = field.FieldName.Namify(), Text = field.NumericValue.ToString("0") });
                        break;
                    case FieldTypes.Datum:
                        customFieldContainer.Children.Add(new DatePicker { Name = field.FieldName.Namify(), SelectedDate = field.DateValue });
                        break;
                    case FieldTypes.JaNee:
                        customFieldContainer.Children.Add(new CheckBox { Name = field.FieldName.Namify(), IsChecked = field.BooleanValue });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                stackPanel.Children.Add(customFieldContainer);
            }
        }

        private void OnHyperlinkClicked(object sender, RoutedEventArgs e)
        {
            var destination = ((Hyperlink)e.OriginalSource).NavigateUri;
            Process.Start(destination.ToString());
        }
    }
}
