using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using GCode3D.Models;
using GCode3D.Models.Program;

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
            PickerViewModel.PreviewCommand = 
                new RelayCommand(() => {
                    Program program = new()
                    {
                        File = PickerViewModel.Current.Selection,
                        Commands = Parser.From(PickerViewModel?.Current?.Selection).ToList(),
                    };

                    PreviewViewModel.Current = program;
                    PreviewViewModel.LoadProgram();

                    // TODO: Remove from here and add to a new command
                    RunningViewModel.Current = program;
                });
                
            RunningViewModel.OnUpdate = 
                new RelayCommand(() => {
                    if(PreviewViewModel.Current == null)
                        return;

                    if(RunningViewModel.Current == null)
                        return;

                    // Trigger only when the program is the same across the viewmodels
                    if(RunningViewModel.Current == PreviewViewModel.Current)
                        PreviewViewModel.Current.CurrentCommand = RunningViewModel.Current.CurrentCommand;

                    Debug.WriteLine($"Current command: [{RunningViewModel.Current.CurrentIndex}] {RunningViewModel.Current.CurrentCommand.Code}");
                });
        }
    }
}