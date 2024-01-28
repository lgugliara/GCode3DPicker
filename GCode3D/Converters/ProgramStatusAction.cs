using System.Globalization;
using System.Windows.Data;
using GCode3D.Models.Program;

namespace GCode3D.Converters
{
    public class ProgramStatusAction : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is Program program ? 
                (
                    program.IsRunning ?
                        "Stop" : 
                        "Run"
                ) : 
                "-";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
