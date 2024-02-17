using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace GCode3D.Converters
{
    public class FileSystemInfoFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value switch
            {
                FileInfo File => $"{File.Length / 1024.0 :F2} KB",
                DirectoryInfo Directory => $"{Directory?.GetFileSystemInfos()?.Length} elements",
                _ => string.Empty
            };

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
