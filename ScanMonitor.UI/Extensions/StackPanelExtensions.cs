using ScanMonitor.Database._Interfaces;
using ScanMonitor.UI.Admin.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace ScanMonitor.UI.Extensions
{
    public static class StackPanelExtensions
    {

        public static void CreateCustomFields(this StackPanel stackPanel, IReadOnlyCollection<ICustomFieldDto> customFields, bool clearBeforeCreating = true)
        {
            if (clearBeforeCreating)
                stackPanel.Children.Clear();

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

        private static UIElement CreateEditControl(ICustomFieldDto field)
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

        private static CheckBox CreateCheckBox(ICustomFieldDto field)
        {
            var checkBox = new CheckBox
            {
                Name = field.FieldName.Namify(),
                VerticalAlignment = VerticalAlignment.Center
            };
            checkBox.SetBinding(ToggleButton.IsCheckedProperty, new Binding("BooleanValue") { Source = field });
            return checkBox;
        }

        private static DatePicker CreateDatePicker(ICustomFieldDto field)
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

        private static TextBox CreateTextBox(ICustomFieldDto field)
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

        private static TextBox CreateNumericBox(ICustomFieldDto field)
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
    }
}
