using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using GCode3D.Components;
using GCode3D.Models.Interfaces;
using HelixToolkit.SharpDX.Core;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;

namespace GCode3D.Models.Program
{
    public class Program : StandardComponent, IRunnable
    {
        private static LineBuilder CreatePivot(Vector3 offset = default)
        {
            var g = new LineBuilder();
            g.AddLine(Vector3.Right + offset, Vector3.Left + offset);
            g.AddLine(Vector3.Down + offset, Vector3.Up + offset);
            g.AddLine(Vector3.BackwardLH + offset, Vector3.ForwardLH + offset);
            return g;
        }

        private FileInfo? _File;
        public FileInfo? File
        {
            get => _File;
            set
            {
                Set(ref _File, value);
                Load();
            }
        }

        private List<Instruction> _Commands = [];
        public List<Instruction> Commands
        {
            get => _Commands;
            private set => Set(ref _Commands, value);
        }

        public List<Instruction> CompletedCommands =>
            Commands.Where(c => c.IsCompleted).ToList();

        public List<Instruction> RemainingCommands =>
            Commands.Where(c => !c.IsCompleted).ToList();

        private Instruction _CurrentCommand = new();
        public Instruction CurrentCommand
        {
            get => _CurrentCommand;
            set
            {
                Set(ref _CurrentCommand, value);
                Update();
            }
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

        private LineGeometryModel3D _Preview =
            new()
            {
                Thickness = 1,
                Smoothness = 2,
                Color = System.Windows.Media.Colors.Blue,
                IsThrowingShadow = false,
            };
        public LineGeometryModel3D Preview
        {
            get => _Preview;
            set => Set(ref _Preview, value);
        }

        private LineGeometryModel3D _Pivot =
            new()
            {
                Thickness = 1,
                Smoothness = 2,
                Color = System.Windows.Media.Colors.Red,
                IsThrowingShadow = false,
                Geometry = CreatePivot().ToLineGeometry3D(),
            };
        public LineGeometryModel3D Pivot
        {
            get => _Pivot;
            set => Set(ref _Pivot, value);
        }

        private void Load()
        {
            if(File == null)
                return;

            Commands = [.. Parser.From(File)];
            Preview = new()
            {
                Thickness = 1,
                Smoothness = 2,
                Color = System.Windows.Media.Colors.Blue,
                IsThrowingShadow = false,
                Geometry = ToLineBuilder().ToLineGeometry3D(),
            };
        }

        private void Update()
        {
            Pivot = new()
            {
                Thickness = 1,
                Smoothness = 2,
                Color = System.Windows.Media.Colors.Red,
                IsThrowingShadow = false,
                Geometry = CreatePivot(CurrentPosition).ToLineGeometry3D(),
            };
        }

        public LineBuilder ToLineBuilder()
            {
                var g = new LineBuilder();
                Commands.ForEach(c =>
                {
                    if(c is MacroInstruction m)
                        m.Commands.ForEach(mc => g.AddLine(mc.From, mc.To));
                    else
                        g.AddLine(c.From, c.To);
                });
                return g;
            }

        #region IGCRunnable
        public bool IsRunning
        {
            get => Task?.Status == TaskStatus.Running;    
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
                    CurrentIndex = index;
                    c.IsRunning = true;
                    c.IsCompleted = false;

                    Application.Current.Dispatcher.Invoke(() => CurrentCommand = c);

                    Stopwatch.Restart();
                    await Task.Delay(50);
                    Stopwatch.Stop();
                    onUpdate?.Execute(null);
                }
            });
        }
        public void Stop() => 
            Task = null;
        #endregion
    }
}
