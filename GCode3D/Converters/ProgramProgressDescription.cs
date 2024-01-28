using System.Globalization;
using System.Windows.Data;
using GCode3D.Models.Program;

namespace GCode3D.Converters
{
    public class ProgramProgressDescription : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is Program program ? 
                (
                    program.Commands.Count > 0 ?
                        (program.CurrentIndex + 1) / (float)program.Commands.Count : 
                        0
                ).ToString("P2") : 
                "-";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
