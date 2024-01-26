using GCode3D.Models.Program;

namespace GCode3D.ViewModels
{
    public class RunningViewModel : StandardViewModel
    {
        public Program? _Current = new();
        public Program? Current
        {
            get => _Current;
            set => Set(ref _Current, value);
        }

        public void LoadRun() =>
            Task.Run(() =>
            {
                Current?.Stop();
                Current?.Start(() => OnPropertyChanged(nameof(Program)));
            });
    }
}