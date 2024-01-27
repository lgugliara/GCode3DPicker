using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace GCode3D.Converters
{
    public class FileNameWithoutExtensionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string fileName)
            {
                return Path.GetFileNameWithoutExtension(fileName);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
