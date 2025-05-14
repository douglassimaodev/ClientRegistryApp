using System.Globalization;

namespace ClientRegistryApp.Common.Validations.Converters;

public class IsNotEmptyVisibleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException("IsNotEmptyVisibleConverter does not support ConvertBack.");
    }
}
