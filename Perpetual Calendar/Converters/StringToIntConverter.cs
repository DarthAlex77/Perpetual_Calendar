using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Perpetual_Calendar.Converters
{
    public class StringToIntConverter: IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            String str = value as String;

            return System.Convert.ToInt32(str);
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            Int32 str = value as Int32? ?? 0;

            return str.ToString();
        }
    }
}