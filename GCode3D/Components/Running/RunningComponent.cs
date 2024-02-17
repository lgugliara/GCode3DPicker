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
        
        public Program? Current { get; set; } =
            new Use<Program>(new Program());

        #endregion

        #region Getters

        public string Action => 
            Current?.IsRunning ?? false ?
                "Stop" :
                "Run";

        #endregion

        #region Methods

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
                            
                            // Update Logger
                            if(Current?.CurrentCommand != null)
                                Application.Current.Dispatcher.Invoke(() => OutputLoggerComponent?.Current?.Add(Current.CurrentCommand));
                        })
                );
        }

        public void ToggleExpand() =>
            IsExpanded = !IsExpanded;

        #endregion
    }
}