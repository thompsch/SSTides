using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace SalishSeaTides;

internal class DateToStrokeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var date = value as DateTime?;
        if (date.HasValue && date.Value.Date == DateTime.Now.Date)
        {
            return 2;
        }

        return 0;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}