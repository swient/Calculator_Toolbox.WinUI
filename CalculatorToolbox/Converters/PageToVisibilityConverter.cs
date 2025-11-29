using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace CalculatorToolbox.Converters
{
    public class PageToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string page && parameter is string target)
                return page == target ? Visibility.Visible : Visibility.Collapsed;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}