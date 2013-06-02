using System;
using System.Windows.Data;

namespace runner.Auxiliar
{
    public class DoubleToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double dValue;
            if (double.TryParse(value.ToString(), out dValue))
            {
                return dValue;
            }
            return double.NaN;
        }
    }
}
