using System;
using System.Globalization;
using Xamarin.Forms;

namespace Tailenders.Converters
{
    public class ItemTappedEventArgsToItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as ItemTappedEventArgs;

            return eventArgs == null
                ? value
                : eventArgs.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
