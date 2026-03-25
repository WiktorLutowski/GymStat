using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GymStat.Converters
{
    class ImageNameToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(ImageSource))
            {
                if (value is string fileName)
                {
                    BitmapImage image = new();

                    image.BeginInit();

                    image.UriSource = new Uri(Path.GetFullPath(Path.Combine("Images", fileName)));
                   
                    image.EndInit();
                    

                    return image;
                }
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
