using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace GCode3D.Converters
{
    public class FileNameWithoutExtension : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is string fileName ? 
                Path.GetFileNameWithoutExtension(fileName) :
                value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
