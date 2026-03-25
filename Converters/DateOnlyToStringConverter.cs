using System.Globalization;
using System.Windows.Data;

namespace GymStat.Converters
{
    class DateOnlyToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                if (value is DateOnly date)
                    return date.ToString("dd.MM.yyyy");
            }

            // Throw an exception if the target type is not string or the value is not DateOnly
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
