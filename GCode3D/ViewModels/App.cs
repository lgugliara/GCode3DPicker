using CommunityToolkit.Mvvm.Input;
using GCode3D.Models.Program;
using System.Diagnostics;

namespace GCode3D.ViewModels
{
    public class AppViewModel : StandardViewModel
    {
        private PickerViewModel _PickerViewModel = new();
        public PickerViewModel PickerViewModel
        {
            get => _PickerViewModel;
            set => Set(ref _PickerViewModel, value);
        }

        private PreviewViewModel _PreviewViewModel = new();
        public PreviewViewModel PreviewViewModel
        {
            get => _PreviewViewModel;
            set => Set(ref _PreviewViewModel, value);
        }

        private RunningViewModel _RunningViewModel = new();
        public RunningViewModel RunningViewModel
        {
            get => _RunningViewModel;
            set => Set(ref _RunningViewModel, value);
        }

        public AppViewModel()
        {
            PickerViewModel.OnSelect =
                new RelayCommand(() =>
                    {
                        Program program = new();
                        PreviewViewModel.Current = RunningViewModel.Current = program;
                        PreviewViewModel.Current.File = PickerViewModel.Current?.Selection;
                    }
                );

            RunningViewModel.OnUpdate =
                new RelayCommand(() =>
                {
                    Debug.WriteLine($"Current command: [{PreviewViewModel?.Current?.CurrentIndex}] {PreviewViewModel?.Current?.CurrentCommand.Code}");
                });
        }
    }
}