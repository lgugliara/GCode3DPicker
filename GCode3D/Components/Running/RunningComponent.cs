using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using GCode3D.Models.Program;

namespace GCode3D.Components
{
    public class RunningComponent : StandardComponent
    {
        public ICommand? OnUpdate { get; set; }
        
        private Program? _Current = new();
        public Program? Current
        {
            get => _Current;
            set => Set(ref _Current, value);
        }

        public void ToggleRun()
        {
            if(Current?.IsRunning ?? false)
                Current?.Stop();
            else
                Current?.Start(
                    new RelayCommand(() =>
                        {
                            OnPropertyChanged(nameof(Current));
                            OnUpdate?.Execute(null);
                        })
                );
        }
    }
}