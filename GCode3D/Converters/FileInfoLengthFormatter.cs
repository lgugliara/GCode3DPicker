using System.Globalization;
using System.Windows.Data;

namespace GCode3D.Converters
{
    public class FileInfoLengthFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is long length ? $"{length / 1024.0 :F2} KB" : string.Empty;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
