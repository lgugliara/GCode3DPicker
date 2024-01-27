using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace GCode3D.Converters
{
    public class IsFileSystemInfoDirectory : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is DirectoryInfo;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
