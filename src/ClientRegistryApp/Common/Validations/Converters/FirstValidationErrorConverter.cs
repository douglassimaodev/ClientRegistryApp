﻿using System.Globalization;

namespace ClientRegistryApp.Common.Validations.Converters
{
    public class FirstValidationErrorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ICollection<string> errors = value as ICollection<string>;
            return errors != null && errors.Count > 0 ? errors.ElementAt(0) : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}