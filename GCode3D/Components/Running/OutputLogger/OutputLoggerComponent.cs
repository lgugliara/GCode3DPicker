using System.Collections.ObjectModel;

namespace GCode3D.Components
{
    public class OutputLoggerComponent : Use<OutputLoggerComponent>
    {
        public ObservableCollection<object>? Current = 
            new Use<ObservableCollection<object>>([]);
    }
}