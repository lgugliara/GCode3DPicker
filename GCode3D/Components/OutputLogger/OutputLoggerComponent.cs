using System.Collections.ObjectModel;

namespace GCode3D.Components
{
    public class OutputLoggerComponent : StandardComponent
    {
        private ObservableCollection<object>? _Current = [];
        public ObservableCollection<object>? Current
        {
            get => _Current;
            set => Set(ref _Current, value);
        }
    }
}