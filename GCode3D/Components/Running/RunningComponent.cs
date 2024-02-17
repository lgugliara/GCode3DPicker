using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using GCode3D.Models.Program;

namespace GCode3D.Components
{
    public class RunningComponent : Use<RunningComponent>
    {
        #region Commands

        public ICommand? OnUpdate { get; set; }

        #endregion

        #region Properties

        public bool IsExpanded { get; set; } =
            new Use<bool>(false);

        public OutputLoggerComponent? OutputLoggerComponent { get; set; } =
            new Use<OutputLoggerComponent>();
        
        public Program? Program { get; set; } =
            new Use<Program>(new());

        #endregion

        #region Getters

        public string Action => 
            Program?.IsRunning ?? false ?
                "Stop" :
                "Run";

        #endregion

        #region Methods

        public void ToggleRun()
        {
            if(Program?.IsRunning ?? false)
                Program?.Stop();
            else
                Program?.Start(
                    new RelayCommand(() =>
                        {
                            OnPropertyChanged(nameof(Program));
                            OnUpdate?.Execute(null);
                            
                            // Update Logger
                            if(Program?.CurrentCommand != null)
                                Application.Current.Dispatcher.Invoke(() => OutputLoggerComponent?.Current?.Add(Program.CurrentCommand));
                        })
                );
        }

        public void ToggleExpand() =>
            IsExpanded = !IsExpanded;

        #endregion
    }
}