using System;
using System.Windows;

namespace ScanMonitor.UI.Converters
{
    class BoolToVisibilityConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //you could implement this if you wil use it ;-)
            throw new NotImplementedException();
        }

    }
}
