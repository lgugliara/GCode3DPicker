using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using GCode3D.Components;
using GCode3D.Models.Interfaces;
using HelixToolkit.SharpDX.Core;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;

namespace GCode3D.Models.Program
{
    public class Program : Use<Program>, IRunnable
    {
        #region Commands

        public IRelayCommand? OnUpdate { get; set; }

        #endregion

        #region Properties

        private FileInfo? _File;
        public FileInfo? File
        {
            get => _File;
            set
            {
                Set(ref _File, value);
                
                if(File == null)
                    return;

                Commands = [.. Parser.From(File)];
                Pivot.Geometry = CreatePivotGeometry();
                Preview.Geometry = CreateCADGeometry();
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
                // Deprecated: This is how you interpolate inside a single instruction, not used since we are updating one time per instruction
                /* var from = CurrentCommand.From;
                var to = CurrentCommand.To;
                var elapsed = Stopwatch.Elapsed.TotalSeconds;
                var lerpFactor = elapsed / (elapsed + 1);
                Vector3.Lerp(ref from, ref to, (float)lerpFactor, out Vector3 position);
                return position; */

                return CurrentCommand.From;
            }
        }

        private LineGeometryModel3D _Preview =
            new()
            {
                Color = System.Windows.Media.Colors.White,
            };
        public LineGeometryModel3D Preview
        {
            get => _Preview;
            set => Set(ref _Preview, value);
        }

        private LineGeometryModel3D _Pivot =
            new()
            {
                Color = System.Windows.Media.Colors.Red,
            };
        public LineGeometryModel3D Pivot
        {
            get => _Pivot;
            set => Set(ref _Pivot, value);
        }

        private void Update()
        {
            Pivot.Geometry = CreatePivotGeometry();

            // TODO: That's fast because it assumes this method is called at every instruction update. If it's not, you will need to update every color from completed instructions
            // Note: Every segment has 2 points, so since we are using instruction index, we need to divide it by 2
            var currentIndex = CurrentIndex * 2;
            var lastIndex = CurrentIndex * 2 - 2;
            var colors = new Color4Collection(Preview.Geometry.Colors);

            // Update last instruction color
            if (lastIndex >= 0)
            {
                colors[lastIndex] = new Color4(0.5f, 0.5f, 0.5f, 1);
                colors[lastIndex + 1] = new Color4(0.5f, 0.5f, 0.5f, 1);
            }
            
            // Update current instruction color
            if (currentIndex < Preview.Geometry.Colors.Count)
            {
                colors[currentIndex] = new Color4(0, 1, 1, 1);
                colors[currentIndex + 1] = new Color4(0, 1, 1, 1);
            }
            
            // Assign colors back to trigger geometry update
            Preview.Geometry.Colors = colors;

            // Execute OnUpdate command
            OnUpdate?.Execute(null);
        }

        private LineGeometry3D CreateCADGeometry()
            {
                var g = CreateCADLineBuilder().ToLineGeometry3D();
                g.Colors = new(g.Positions.Select(p => new Color4(0, 0, 1, 1)));
                return g;
            }

        private LineBuilder CreateCADLineBuilder()
            {
                var g = new LineBuilder();
                Commands.ForEach(c =>
                {
                if (c is MacroInstruction m)
                        m.Commands.ForEach(mc => g.AddLine(mc.From, mc.To));
                    else
                        g.AddLine(c.From, c.To);
                });
                return g;
            }

        private LineGeometry3D CreatePivotGeometry() =>
            CreatePivotLineBuilder().ToLineGeometry3D();

        private LineBuilder CreatePivotLineBuilder()
        {
            var g = new LineBuilder();
            g.AddLine(Vector3.Right + CurrentPosition, Vector3.Left + CurrentPosition);
            g.AddLine(Vector3.Down + CurrentPosition, Vector3.Up + CurrentPosition);
            g.AddLine(Vector3.BackwardLH + CurrentPosition, Vector3.ForwardLH + CurrentPosition);
            return g;
        }

        #endregion

        #region IRunnable
        public bool IsRunning
        {
            get => Task != null && Task?.Status == TaskStatus.Running;    
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
                    await Task.Delay(200);
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
