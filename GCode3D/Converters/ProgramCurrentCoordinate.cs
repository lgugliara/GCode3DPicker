using System.Globalization;
using System.Windows.Data;
using SharpDX;

namespace GCode3D.Converters
{
    public class ProgramCurrentCoordinate : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is float coord ? 
                $"{coord.ToString("F4")}" : 
                "-";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public class ProgramCurrentCoordinateX : ProgramCurrentCoordinate
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is Vector3 coord ? 
                base.Convert(coord.X, targetType, parameter, culture) : 
                "-";
    }
    
    public class ProgramCurrentCoordinateY : ProgramCurrentCoordinate
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is Vector3 coord ? 
                base.Convert(coord.X, targetType, parameter, culture) : 
                "-";
    }
    
    public class ProgramCurrentCoordinateZ : ProgramCurrentCoordinate
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is Vector3 coord ? 
                base.Convert(coord.X, targetType, parameter, culture) : 
                "-";
    }
}
