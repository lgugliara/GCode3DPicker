using System.Diagnostics;
using System.Windows.Input;
using GCode3D.Models.Picker;
using GCode3D.ViewModels;
using HelixToolkit.SharpDX.Core;
using SharpDX;

namespace GCode3D.Models.Program
{
    public class Program : StandardViewModel, IRunnable
    {
        private File _File = new();
        public File File
        {
            get => _File;
            set => Set(ref _File, value);
        }

        private List<StatelessCommand> _Commands = [];
        public List<StatelessCommand> Commands
        {
            get => _Commands;
            set => Set(ref _Commands, value);
        }

        private StatelessCommand _CurrentCommand = new();
        public StatelessCommand CurrentCommand
        {
            get => _CurrentCommand;
            set => Set(ref _CurrentCommand, value);
        }
        
        private int _CurrentIndex = 0;
        public int CurrentIndex
        {
            get => _CurrentIndex;
            set => Set(ref _CurrentIndex, value);
        }

        public Stopwatch Stopwatch { get; set; } = new();

        private Task? _Task;
        private Task? Task
        {
            get => _Task;
            set {
                Set(ref _Task, value);
                OnPropertyChanged(nameof(Action));
            }
        }
    
        public Vector3 CurrentPosition
        {
            get {
                var from = CurrentCommand.From;
                var to = CurrentCommand.To;
                Vector3.Lerp(ref from, ref to, (float)Stopwatch.Elapsed.TotalSeconds, out Vector3 position);
                return position;
            }
        }

        public string Progress
        {
            get 
            {
                var factor = Commands.Count > 0 ?
                    CurrentIndex * 100f / Commands.Count :
                    float.NaN;
                return factor.ToString("n2") ?? "-";
            }
        }
        
        public string Description
        {
            get => string.Join("\t", new List<string> {
                $"[{CurrentIndex}/{Commands.Count - 1}] {Progress}%",
                $"{CurrentCommand.Code}",
            });
        }

        public string Action
        {
            get => 
                IsRunning ? 
                    "Stop" : 
                    "Run";
        }

        public async Task<LineBuilder> ToLineBuilder() =>
            await Task.Run(() => {
                var g = new LineBuilder();
                Commands.ForEach(c => g.AddLine(c.From, c.To));
                return g;
            });

        #region IGCRunnable
        public bool IsRunning
        {
            get => CurrentCommand.IsRunning;    
        }
        public void Start(ICommand? onUpdate)
        {
            Task = Task.Run(async () => 
            {
                int index = -1;
                foreach (var c in Commands)
                {
                    CurrentCommand.IsRunning = false;
                    CurrentCommand.IsCompleted = true;
                    index++;

                    CurrentCommand = c;
                    CurrentIndex = index;
                    CurrentCommand.IsRunning = true;

                    Stopwatch.Restart();
                    await Task.Delay(20);
                    onUpdate?.Execute(null);
                    Stopwatch.Stop();
                }
            });
        }
        public void Stop() => CurrentCommand.IsRunning = false;
        #endregion
    }
}
