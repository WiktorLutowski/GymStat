using System.Globalization;
using System.Windows.Data;

namespace GymStat.Converters
{
    class IndexToSeriesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is int index)
                return index + 1;


            // Throw an exception if the target type is not string or the value is not DateOnly
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
