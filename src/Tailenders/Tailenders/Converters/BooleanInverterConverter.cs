using System;
using System.Globalization;
using Xamarin.Forms;

namespace Tailenders.Converters
{
    public class BooleanInverterConverter : IValueConverter
    {
        public bool DefaultValue { get; set; }
        public bool TargetNullValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return InvertBool(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return InvertBool(value);
        }

        private object InvertBool(object value)
        {
            if (value == null)
                return TargetNullValue;

            if (value is bool)
            {
                return !(bool)value;
            }

            var boolValue = false;
            var parseSuccess = Boolean.TryParse(value.ToString(), out boolValue);

            if (parseSuccess)
                return !boolValue;

            return DefaultValue;
        }
    }
}
