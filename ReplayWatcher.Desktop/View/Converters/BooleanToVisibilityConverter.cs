using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace ReplayWatcher.Desktop.View.Converters;

public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool val || targetType != typeof(Visibility))
        {
            throw new ArgumentException();
        }

        if (parameter is string invertedParameter && bool.TryParse(invertedParameter, out var isInverted))
        {
            val = isInverted ? !val : val;
        }
        return val ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}