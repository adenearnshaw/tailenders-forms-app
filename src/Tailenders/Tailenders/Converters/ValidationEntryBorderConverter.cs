using System;
using System.Globalization;
using Xamarin.Forms;

namespace Tailenders.Converters
{
    public class ValidationEntryBorderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isValid = (value as bool?);

            if (!isValid.HasValue)
                return Color.FromHex("#e3e3e3");
            else if (!isValid.Value)
                return Color.FromHex("#CE1414");
            else
                return Color.FromHex("#00FF00");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
