using CommunityToolkit.Mvvm.Input;
using GCode3D.Models.Program;
using System.Diagnostics;

namespace GCode3D.Components
{
    public class MainWindowComponent : StandardComponent
    {
        private PickerComponent _PickerComponent = new();
        public PickerComponent PickerComponent
        {
            get => _PickerComponent;
            set => Set(ref _PickerComponent, value);
        }

        private PreviewComponent _PreviewComponent = new();
        public PreviewComponent PreviewComponent
        {
            get => _PreviewComponent;
            set => Set(ref _PreviewComponent, value);
        }

        private RunningComponent _RunningComponent = new();
        public RunningComponent RunningComponent
        {
            get => _RunningComponent;
            set => Set(ref _RunningComponent, value);
        }

        public MainWindowComponent()
        {
            PickerComponent.OnSelect =
                new RelayCommand(() =>
                    {
                        Program program = new();
                        PreviewComponent.Current = RunningComponent.Current = program;
                        PreviewComponent.Current.File = PickerComponent.Current?.Selection;
                    }
                );

            RunningComponent.OnUpdate =
                new RelayCommand(() =>
                {
                    Debug.WriteLine($"Current command: [{PreviewComponent?.Current?.CurrentIndex}] {PreviewComponent?.Current?.CurrentCommand.Code}");
                });
        }
    }
}